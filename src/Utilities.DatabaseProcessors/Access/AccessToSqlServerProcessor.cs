using Core.Providers;
using Extensions;
using FluentCommander;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;

namespace Utilities.DatabaseProcessors.Access
{
    [Processor]
    public class AccessToSqlServerProcessor : UtilitiesBase, IProcessor
    {
        private readonly IDatabaseCommander _databaseCommander;
        private readonly Dictionary<string, string> _databaseColumns;
        private readonly SqlServerFileProcessorSettings _settings;

        public AccessToSqlServerProcessor(
            IDatabaseCommanderFactory databaseCommanderFactory,
            IConnectionStringProvider connectionStringProvider)
        {
            _databaseCommander = databaseCommanderFactory.Create(_settings.ConnectionStringName);
            _settings = new SqlServerFileProcessorSettings
            {
                ConnectionStringName = Constants.Configuration.ConnectionString.DefaultConnection,
                FilePath = connectionStringProvider.Get("AccessConnection"),
                SchemaName = "issue",
                TableName = "ESGBeforeOpSolve",
                PrimaryKeyColumnName = "ESGBeforeOpSolveID",
                CreatedBy = "sferguson",
                IsAppendAuditFields = true
            };

            _databaseColumns = new Dictionary<string, string>();

            Init();
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            bool tableExists = _databaseCommander.ExecuteScalar<int>($"SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{_settings.SchemaName}' AND TABLE_NAME = '{_settings.TableName}'") > 0;

            if (tableExists)
                throw new Exception($"Table {_settings.TableNameQualified} already exists");

            DataTable dataTable = GetDataTableFromAccess();

            PrependPrimaryKeyIfNeeded(dataTable);

            AppendAuditFieldsIfNeeded(dataTable);

            SetColumnsToCreate(dataTable);

            CreateTable();

            await _databaseCommander.BuildCommand()
                .ForBulkCopy()
                .Into(_settings.TableNameQualified)
                .From(dataTable)
                .Mapping(mapping => mapping.UseAutoMap())
                .ExecuteAsync(cancellationToken);
        }

        public DataTable GetDataTableFromAccess()
        {
            string sql = "SELECT * FROM CustAcctDet";

            var dataTable = new DataTable();

            using OleDbConnection conn = new OleDbConnection(_settings.FilePath);

            conn.Open();

            using OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);

            adapter.Fill(dataTable);

            return dataTable;
        }

        private void Init()
        {
            if (!string.IsNullOrEmpty(_settings.PrimaryKeyColumnName))
            {
                _databaseColumns.Add(_settings.PrimaryKeyColumnName, "UNIQUEIDENTIFIER NOT NULL PRIMARY KEY");
            }
        }

        private void PrependPrimaryKeyIfNeeded(DataTable dataTable)
        {
            if (string.IsNullOrEmpty(_settings.PrimaryKeyColumnName))
                return;

            dataTable.Columns.Add(_settings.PrimaryKeyColumnName, typeof(Guid));

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow[_settings.PrimaryKeyColumnName] = Guid.NewGuid();
            }
        }

        private void AppendAuditFieldsIfNeeded(DataTable dataTable)
        {
            if (!_settings.IsAppendAuditFields)
                return;

            dataTable.Columns.Add("CreatedBy", typeof(string));
            dataTable.Columns.Add("CreatedDate", typeof(DateTime));
            dataTable.Columns.Add("ModifiedBy", typeof(string));
            dataTable.Columns.Add("ModifiedDate", typeof(DateTime));

            DateTime createdDate = DateTime.UtcNow;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["CreatedBy"] = _settings.CreatedBy;
                dataRow["CreatedDate"] = createdDate;
            }
        }

        private void SetColumnsToCreate(DataTable dataTable)
        {
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                dataColumn.ColumnName = dataColumn.ColumnName.Without(" ");

                if (_databaseColumns.ContainsKey(dataColumn.ColumnName))
                    continue;

                string dataType = GetDataType(dataTable, dataColumn.ColumnName);

                _databaseColumns.Add(dataColumn.ColumnName, dataType);
            }
        }

        private string GetDataType(DataTable dataTable, string columnName)
        {
            if (dataTable.Columns[columnName].DataType == typeof(DateTime))
                return "DATETIME NULL";

            if (dataTable.Columns[columnName].DataType == typeof(int))
                return "INT NULL";

            string varCharLength = GetVarCharLength(dataTable, columnName);

            return $"VARCHAR ({varCharLength}) NULL";
        }

        private string GetVarCharLength(DataTable dataTable, string columnName)
        {
            int maxStringLength = 0;

            IEnumerable<string> values = dataTable.AsEnumerable()
                .Select(row => row[columnName]).OfType<string>();

            if (values.Any())
            {
                maxStringLength = values.Max(val => val.Length);
            }

            if (maxStringLength < 60)
                return "100";

            if (maxStringLength < 160)
                return "200";

            if (maxStringLength < 400)
                return "500";

            if (maxStringLength < 800)
                return "1000";

            if (maxStringLength < 1500)
                return "2000";

            if (maxStringLength < 3000)
                return "4000";

            return "MAX";
        }

        private void CreateTable()
        {
            var columnsToCreate = new List<string>();

            foreach (KeyValuePair<string, string> kvp in _databaseColumns)
            {
                columnsToCreate.Add($"{Environment.NewLine}    [{kvp.Key}]    {kvp.Value}");
            }

            string sql = $"CREATE TABLE {_settings.TableNameQualified} ({string.Join(",", columnsToCreate)})";

            _databaseCommander.ExecuteNonQuery(sql);

            if (_settings.IsAppendAuditFields)
            {
                _databaseCommander.ExecuteNonQuery($"ALTER TABLE {_settings.TableNameQualified} ALTER COLUMN CreatedBy    VARCHAR(100)  NOT NULL");
                _databaseCommander.ExecuteNonQuery($"ALTER TABLE {_settings.TableNameQualified} ALTER COLUMN CreatedDate  DATETIME      NOT NULL");
            }
        }
    }
}
