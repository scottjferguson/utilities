using Core.FileHandling;
using FluentCommander;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.SqlServer
{
    [Processor]
    public class CsvToSqlServerFileProcessor : FileProcessorBase, IProcessor
    {
        private readonly IDelimitedFileHandler _delimitedFileHandler;

        public CsvToSqlServerFileProcessor(
            IDatabaseCommanderFactory databaseCommandFactory,
            IDelimitedFileHandler delimitedFileHandler)
        {
            Settings = new SqlServerFileProcessorSettings
            {
                ConnectionStringName = Constants.Configuration.ConnectionString.DefaultConnection,
                FilePath = @"C:\Users\scott\Documents\ExcelDataSource\subscribed_members_export_a6ee01d8d2.csv",
                SchemaName = "issue",
                TableName = "EmailListBillCreditOriginal",
                PrimaryKeyColumnName = "EmailListBillCreditID",
                PrimaryKeyColumnType = typeof(Guid),
                CreatedBy = "sferguson",
                IsAppendAuditFields = true
            };

            _delimitedFileHandler = delimitedFileHandler;
            DatabaseCommander = databaseCommandFactory.Create(Settings.ConnectionStringName);
            DatabaseColumns = new Dictionary<string, string>();

            Init();
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            bool tableExists = DatabaseCommander.ExecuteScalar<int>($"SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{Settings.SchemaName}' AND TABLE_NAME = '{Settings.TableName}'") > 0;

            if (tableExists)
                throw new Exception($"Table {Settings.TableNameQualified} already exists");

            DataTable dataTable = _delimitedFileHandler.GetFileAsDataTable(Settings.FilePath);

            PrependPrimaryKeyIfNeeded(dataTable);

            AppendAuditFieldsIfNeeded(dataTable);

            SetColumnsToCreate(dataTable);

            CreateTable();

            await DatabaseCommander.BuildCommand()
                .ForBulkCopy()
                .Into(Settings.TableNameQualified)
                .From(dataTable)
                .Mapping(mapping => mapping.UseAutoMap())
                .ExecuteAsync(cancellationToken);
        }
    }
}
