using Extensions;
using FluentCommander;
using Processor;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;

namespace Utilities.DatabaseProcessors.Reconcile
{
    [Processor]
    public class ReconcileCleanupProcessor : ProcessorBase
    {
        private readonly IDatabaseCommander _commander;

        public ReconcileCleanupProcessor(IDatabaseCommanderFactory commanderFactory)
        {
            _commander = commanderFactory.Create("AccountDatabase");
        }

        public override async Task ProcessAsync(CancellationToken cancellationToken)
        {
            await CleanupDuplicateStatements(cancellationToken);

            await CleanupDuplicateTimelineEvents(cancellationToken);
        }

        private async Task CleanupDuplicateStatements(CancellationToken cancellationToken)
        {
            DataTable dataTable = await _commander.ExecuteSqlAsync(GetDuplicateStatementsSql, cancellationToken);

            if (dataTable.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                string sourceLink = dataRow.GetSafeString("SourceLink");
                int count = dataRow.GetSafeInt32("Count");

                DataTable statementIds = await _commander.ExecuteSqlAsync($"SELECT TOP {count - 1} [StatementId] FROM [dbo].[Statement] (NOLOCK) WHERE [SourceLink] = '{sourceLink}' AND [IsLatest] = 0 ORDER BY [StatementId] DESC", cancellationToken);

                foreach (DataRow statementRow in statementIds.Rows)
                {
                    long statementId = statementRow.GetSafeInt64("StatementId");

                    await _commander.ExecuteNonQueryAsync($"DELETE FROM [dbo].[Statement] WHERE [StatementId] = {statementId}", cancellationToken);
                }
            }
        }

        private async Task CleanupDuplicateTimelineEvents(CancellationToken cancellationToken)
        {
            DataTable dataTable = await _commander.ExecuteSqlAsync(GetDuplicateTimelineEventsSql, cancellationToken);

            if (dataTable.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                long accountId = dataRow.GetSafeInt64("AccountId");
                long timelineEventTypeId = dataRow.GetSafeInt64("TimelineEventTypeId");
                DateTime eventDate = dataRow.GetSafeDateTime("EventDate");
                string billerCode = dataRow.GetSafeString("BillerCode");
                int count = dataRow.GetSafeInt32("Count");

                DataTable timelineEvents = await _commander.ExecuteSqlAsync($"SELECT TOP {count - 1} [TimelineEventId] FROM [dbo].[TimelineEvent] t (NOLOCK) INNER JOIN [Account] (NOLOCK) a on t.[AccountId] = a.[AccountId] WHERE t.[AccountId] = {accountId} AND [TimelineEventTypeId] = {timelineEventTypeId} AND CONVERT(DATE, [EventDate]) = '{eventDate.ToShortDateString()}' AND [BillerCode] = '{billerCode}' ORDER BY [TimelineEventId] DESC", cancellationToken);

                foreach (DataRow timelineEventRow in timelineEvents.Rows)
                {
                    long timelineEventId = timelineEventRow.GetSafeInt64("TimelineEventId");

                    await _commander.ExecuteNonQueryAsync($"DELETE FROM [dbo].[TimelineEvent] WHERE [TimelineEventId] = {timelineEventId}", cancellationToken);
                }
            }
        }

        private string GetDuplicateStatementsSql =>
@"SELECT [SourceLink], COUNT(1) [Count]
FROM [dbo].[Statement] (NOLOCK)
GROUP BY [SourceLink]
HAVING COUNT(1) > 1
ORDER BY COUNT(1) DESC";

        private string GetDuplicateTimelineEventsSql =>
@"SELECT
    [AccountId]
  , [TimelineEventTypeId]
  , [EventTypeName]
  , CONVERT(DATE, [EventDate]) [EventDate]
  , [BillerCode]
  , COUNT(1) [Count]
FROM [dbo].[vwAccountTimeline]
GROUP BY 
    [AccountId]
  , [TimelineEventTypeId]
  , [EventTypeName]
  , CONVERT(DATE, [EventDate])
  , [BillerCode]
HAVING COUNT(1) > 1
ORDER BY COUNT(1) DESC";
    }
}
