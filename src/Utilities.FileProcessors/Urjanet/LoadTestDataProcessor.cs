using Core.Application;
using Core.Plugins.Utilities.FileHandling.Delimited;
using FluentCommander;
using FluentCommander.StoredProcedure;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.Urjanet
{
    [Processor(Name = "LoadTestData")]
    public class LoadTestDataProcessor : FileProcessorBase, IProcessor
    {
        public LoadTestDataProcessor(IDatabaseCommanderFactory databaseCommandFactory)
        {
            Settings = new SqlServerFileProcessorSettings
            {
                ConnectionStringName = Constants.Configuration.ConnectionString.UrjanetDatabase,
                FilePath = @"C:\Users\scott\Desktop\temp\Adhoc_Guroo_08_04_20_15_22.urj",
                SchemaName = "dbo",
                TableName = "UrjanetLoadStage"
            };

            DatabaseCommander = databaseCommandFactory.Create(Settings.ConnectionStringName);
        }
        
        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            await DatabaseCommander.ExecuteNonQueryAsync($"DELETE FROM {Settings.TableNameQualified}", cancellationToken);

            DataTable dataTable = new GenericParsingDelimitedFileHandler().GetFileAsDataTable(Settings.FilePath, '~');

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
    }
}
