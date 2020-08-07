using FluentCommander;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;

namespace Utilities.DatabaseProcessors.DataRowProcessors.Base
{
    public abstract class DataRowProcessorBase : ProcessorBase<DataTable>
    {
        protected readonly IDatabaseCommander DatabaseCommander;

        protected abstract string RunSql();
        protected abstract void ProcessDataRow(DataRow dataRow);

        public override async Task ProcessAsync(CancellationToken cancellationToken)
        {
            try
            {
                string sql = RunSql();

                DataTable dataTable = await DatabaseCommander.ExecuteSqlAsync(sql, cancellationToken);

                PreProcess(dataTable);

                for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
                {
                    ProcessDataRow(dataTable.Rows[i]);
                }

                PostProcess(dataTable);
            }
            catch (Exception e)
            {
                OnException(e);
            }
        }

        protected override void OnException(Exception e)
        {
            Rollback();

            base.OnException(e);
        }

        protected virtual void Rollback()
        {

        }
    }
}
