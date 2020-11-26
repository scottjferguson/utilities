using Extensions;
using FluentCommander;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain.Framework;

namespace Utilities.DatabaseProcessors.DatabaseTableProcessor
{
    [Processor]
    public class NullColumnDatabaseTableProcessor : ProcessorBase
    {
        private readonly IDatabaseCommander _databaseCommander;
        private readonly string _schemaName = "dbo";
        private readonly string _tableName = "EnrollmentRequests";

        public NullColumnDatabaseTableProcessor(IDatabaseCommanderFactory databaseCommanderFactory)
        {
            _databaseCommander = databaseCommanderFactory.Create();
        }

        public override async Task ProcessAsync(CancellationToken cancellationToken)
        {
            var columnsWithOnlyNulls = new List<string>();

            foreach (string nullableColumn in await GetNullableColumns(cancellationToken))
            {
                bool isColumnHasOnlyNull = IsColumnHasOnlyNull(nullableColumn);

                if (isColumnHasOnlyNull)
                {
                    columnsWithOnlyNulls.Add(nullableColumn);
                }
            }

            columnsWithOnlyNulls.ForEach(Console.WriteLine);
        }

        private async Task<List<string>> GetNullableColumns(CancellationToken cancellationToken)
        {
            string sql =
@"SELECT
  SCHEMA_NAME(o.schema_id)    AS [Schema]
, o.name                      AS [Object]
, c.column_id                 AS [ObjectOrder]
, c.name                      AS [ObjectName]
, TYPE_NAME(c.user_type_id)   AS [ObjectDataType]
, -1                          AS [ObjectMaxLength]
, CAST(c.is_nullable AS int)  AS [ObjectNullable]
FROM sys.objects o
   INNER JOIN sys.columns c ON o.object_id = c.object_id
WHERE c.object_id = OBJECT_ID('{0}.{1}')
ORDER BY 1, 2, 3";

            DataTable dataTable = await _databaseCommander
                .ExecuteSqlAsync(string.Format(sql, _schemaName, _tableName), cancellationToken);

            return dataTable.AsEnumerable()
                .Where(dr => dr.GetSafeBoolean("ObjectNullable"))
                .Select(dr => dr.GetSafeString("ObjectName"))
                .ToList();
        }

        private bool IsColumnHasOnlyNull(string nullableColumn)
        {
            string sql =
@"SELECT COUNT(1)
FROM {0}.{1}
WHERE {2} IS NOT NULL";

            int count = _databaseCommander.ExecuteScalar<int>(string.Format(sql, _schemaName, _tableName, nullableColumn));

            return count == 0;
        }
    }
}
