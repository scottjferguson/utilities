using System;

namespace Utilities.Common
{
    public class SqlServerFileProcessorSettings
    {
        public SqlServerFileProcessorSettings()
        {
            StartTime = DateTime.UtcNow;
        }

        public string ConnectionStringName { get; set; }

        public string FilePath { get; set; }

        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public string PrimaryKeyColumnName { get; set; }

        public Type PrimaryKeyColumnType { get; set; }

        public string CreatedBy { get; set; }

        public bool IsAppendAuditFields { get; set; }

        public DateTime StartTime { get; set; }

        public string TableNameQualified => $"[{SchemaName}].[{TableName}]";
    }
}
