using Core.FileHandling;
using FluentCommander;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain;
using Utilities.Domain.Framework;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.SqlServer
{
    [Processor]
    public class ExcelToSqlServerFileProcessor : FileProcessorBase, IProcessor
    {
        private readonly IExcelFileHandler _excelFileHandler;

        public ExcelToSqlServerFileProcessor(
            IDatabaseCommanderFactory databaseCommandFactory,
            IExcelFileHandler excelFileHandler)
        {
            Settings = new SqlServerFileProcessorSettings
            {
                ConnectionStringName = Constants.Configuration.ConnectionString.DefaultConnection,
                FilePath = @"C:\Users\scott\Documents\ExcelDataSource\do-not-email.xlsx",
                SchemaName = "issue",
                TableName = "EmailListBillCreditOriginal",
                PrimaryKeyColumnName = "EmailListBillCreditID",
                PrimaryKeyColumnType = typeof(Guid),
                CreatedBy = "sferguson",
                IsAppendAuditFields = true
            };

            _excelFileHandler = excelFileHandler;
            DatabaseCommander = databaseCommandFactory.Create(Settings.ConnectionStringName);
            DatabaseColumns = new Dictionary<string, string>();

            Init();

            // Use this to setup column names and data types where the auto-generation doesn't do what you need
            //_databaseColumns.Add("EnrollmentRequestRewardID", "UNIQUEIDENTIFIER NULL");
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            bool tableExists = DatabaseCommander.ExecuteScalar<int>($"SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{Settings.SchemaName}' AND TABLE_NAME = '{Settings.TableName}'") > 0;

            if (tableExists)
                throw new Exception($"Table {Settings.TableNameQualified} already exists");

            DataTable dataTable = _excelFileHandler.GetWorksheetAsDataTable(Settings.FilePath);

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
