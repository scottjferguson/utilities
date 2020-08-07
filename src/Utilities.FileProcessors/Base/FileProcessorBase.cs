using FluentCommander;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utilities.Common;

namespace Utilities.FileProcessors.Base
{
    public class FileProcessorBase : UtilitiesBase
    {
        protected IDatabaseCommander DatabaseCommander;
        protected Dictionary<string, string> DatabaseColumns;
        protected SqlServerFileProcessorSettings Settings;

        protected void Init()
        {
            if (!string.IsNullOrEmpty(Settings.PrimaryKeyColumnName))
            {
                if (Settings.PrimaryKeyColumnType == typeof(int))
                {
                    DatabaseColumns.Add(Settings.PrimaryKeyColumnName, "INT IDENTITY(1, 1) NOT NULL PRIMARY KEY");
                }
                else if (Settings.PrimaryKeyColumnType == typeof(long))
                {
                    DatabaseColumns.Add(Settings.PrimaryKeyColumnName, "BIGINT IDENTITY(1, 1) NOT NULL PRIMARY KEY");
                }
                else
                {
                    DatabaseColumns.Add(Settings.PrimaryKeyColumnName, "UNIQUEIDENTIFIER NOT NULL PRIMARY KEY");
                }
            }
        }

        protected void PrependPrimaryKeyIfNeeded(DataTable dataTable)
        {
            if (string.IsNullOrEmpty(Settings.PrimaryKeyColumnName))
                return;

            if (Settings.PrimaryKeyColumnType == typeof(int))
            {
                dataTable.Columns.Add(Settings.PrimaryKeyColumnName, typeof(int));
            }
            else if (Settings.PrimaryKeyColumnType == typeof(long))
            {
                dataTable.Columns.Add(Settings.PrimaryKeyColumnName, typeof(long));
            }
            else
            {
                dataTable.Columns.Add(Settings.PrimaryKeyColumnName, typeof(Guid));

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    dataRow[Settings.PrimaryKeyColumnName] = Guid.NewGuid();
                }
            }
        }

        protected void AppendAuditFieldsIfNeeded(DataTable dataTable)
        {
            if (!Settings.IsAppendAuditFields)
                return;

            dataTable.Columns.Add("CreatedBy", typeof(string));
            dataTable.Columns.Add("CreatedDate", typeof(DateTime));
            dataTable.Columns.Add("ModifiedBy", typeof(string));
            dataTable.Columns.Add("ModifiedDate", typeof(DateTime));

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["CreatedBy"] = Settings.CreatedBy;
                dataRow["CreatedDate"] = Settings.StartTime;
            }
        }

        protected void SetColumnsToCreate(DataTable dataTable)
        {
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                dataColumn.ColumnName = Core.Plugins.Extensions.StringExtensions.Remove(dataColumn.ColumnName, " ");

                if (DatabaseColumns.ContainsKey(dataColumn.ColumnName))
                    continue;

                string dataType = GetDataType(dataTable, dataColumn.ColumnName);

                DatabaseColumns.Add(dataColumn.ColumnName, dataType);
            }
        }

        protected string GetDataType(DataTable dataTable, string columnName)
        {
            if (dataTable.Columns[columnName].DataType == typeof(DateTime))
                return "DATETIME NULL";

            if (dataTable.Columns[columnName].DataType == typeof(int))
                return "INT NULL";

            string varCharLength = GetVarCharLength(dataTable, columnName);

            return $"VARCHAR ({varCharLength}) NULL";
        }

        protected string GetVarCharLength(DataTable dataTable, string columnName)
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

        protected void CreateTable()
        {
            var columnsToCreate = new List<string>();

            foreach (KeyValuePair<string, string> kvp in DatabaseColumns)
            {
                columnsToCreate.Add($"{Environment.NewLine}    [{kvp.Key}]    {kvp.Value}");
            }

            string sql = $"CREATE TABLE {Settings.TableNameQualified} ({string.Join(",", columnsToCreate)})";

            DatabaseCommander.ExecuteNonQuery(sql);

            if (Settings.IsAppendAuditFields)
            {
                DatabaseCommander.ExecuteNonQuery($"ALTER TABLE {Settings.TableNameQualified} ALTER COLUMN CreatedBy    VARCHAR(100)  NOT NULL");
                DatabaseCommander.ExecuteNonQuery($"ALTER TABLE {Settings.TableNameQualified} ALTER COLUMN CreatedDate  DATETIME      NOT NULL");
            }
        }
    }
}
