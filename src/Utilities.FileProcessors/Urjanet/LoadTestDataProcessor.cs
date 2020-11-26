using Core.FileHandling;
using FluentCommander;
using FluentCommander.StoredProcedure;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain;
using Utilities.Domain.Framework;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.Urjanet
{
    [Processor]
    public class LoadTestDataProcessor : FileProcessorBase, IProcessor
    {
        private readonly IDelimitedFileHandler _delimitedFileHandler;

        public LoadTestDataProcessor(
            IDatabaseCommanderFactory databaseCommandFactory,
            IDelimitedFileHandler delimitedFileHandler)
        {
            Settings = new SqlServerFileProcessorSettings
            {
                ConnectionStringName = Constants.Configuration.ConnectionString.UrjanetDatabase,
                FilePath = @"C:\Users\scott\OneDrive\Guroo\Projects\2020-07-31 - Urjanet\URJ\Guroo_08_10_20_09_15.urj",
                SchemaName = "dbo",
                TableName = "UrjanetLoadStage"
            };

            _delimitedFileHandler = delimitedFileHandler;
            DatabaseCommander = databaseCommandFactory.Create(Settings.ConnectionStringName);
        }
        
        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            //await DatabaseCommander.ExecuteNonQueryAsync($"DELETE FROM {Settings.TableNameQualified}", cancellationToken);

            DataTable dataTable = _delimitedFileHandler.GetFileAsDataTable(Settings.FilePath, '~');

            await DatabaseCommander.BuildCommand()
                .ForBulkCopy()
                .Into(Settings.TableNameQualified)
                .From(dataTable)
                .Mapping(mapping => mapping.UseAutoMap())
                .ExecuteAsync(cancellationToken);

            StoredProcedureResult result = await DatabaseCommander.BuildCommand()
                .ForStoredProcedure("[dbo].[LoadData]")
                .AddInputParameter("DataFile", Settings.FilePath)
                .ExecuteAsync(cancellationToken);

            if (result.GetReturnParameter<string>() == "-1")
            {
                throw new Exception("[dbo].[LoadData] encountered error");
            }
        }

        public ColumnMapping MapDataTableToTable(string tableName, DataTable dataTable)
        {
            DataTable emptyDataTable = DatabaseCommander.ExecuteSql($"SELECT * FROM {tableName} WHERE 1 = 0");

            List<string> databaseColumnNames = emptyDataTable.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();
            List<string> fileColumnNames = dataTable.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();

            var columnMapping = new ColumnMapping();

            // Ensure proper casing of source columns
            foreach (string fileColumnName in fileColumnNames)
            {
                ColumnMap columnMap = columnMapping.ColumnMaps.FirstOrDefault(cm => cm.Source.Equals(fileColumnName, StringComparison.InvariantCultureIgnoreCase));

                if (columnMap != null && columnMap.Source != fileColumnName)
                {
                    columnMap.Source = fileColumnName;
                }
            }

            // Ensure proper casing of destination columns
            foreach (string databaseColumnName in databaseColumnNames)
            {
                ColumnMap columnMap = columnMapping.ColumnMaps.FirstOrDefault(cm => cm.Destination.Equals(databaseColumnName, StringComparison.InvariantCultureIgnoreCase));

                if (columnMap != null && columnMap.Destination != databaseColumnName)
                {
                    columnMap.Destination = databaseColumnName;
                }
            }

            // Ensure existence of source columns
            foreach (string sourceColumnName in columnMapping.ColumnMaps.Select(cm => cm.Source))
            {
                if (!fileColumnNames.Contains(sourceColumnName))
                {
                    throw new InvalidOperationException($"The source column '{sourceColumnName}' does not exist");
                }
            }

            // Ensure existence of destination columns
            foreach (string destinationColumnName in columnMapping.ColumnMaps.Select(cm => cm.Destination))
            {
                if (!databaseColumnNames.Contains(destinationColumnName))
                {
                    throw new InvalidOperationException($"The destination column '{destinationColumnName}' does not exist");
                }
            }

            // AutoMap any columns that are not already mapped from the source to the destination
            foreach (string fileColumnName in fileColumnNames)
            {
                string databaseColumnName = databaseColumnNames.FirstOrDefault(s => s.Equals(fileColumnName, StringComparison.InvariantCultureIgnoreCase));

                if (databaseColumnName != null && columnMapping.ColumnMaps.All(cm => cm.Source != fileColumnName) && columnMapping.ColumnMaps.All(cm => cm.Destination != databaseColumnName))
                {
                    columnMapping.ColumnMaps.Add(new ColumnMap(fileColumnName, databaseColumnName));
                }
            }

            return columnMapping;
        }
    }
}
