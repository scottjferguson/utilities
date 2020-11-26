using Core.FileHandling;
using Extensions;
using FluentCommander;
using FluentCommander.SqlNonQuery;
using Processor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain.Framework;

namespace Utilities.FileProcessors.XVerify
{
    [Processor]
    public class XVerifyMailingAddressValidFileProcessor : UtilitiesBase, IProcessor
    {
        private readonly IDatabaseCommander _databaseCommander;
        private readonly IDelimitedFileHandler _delimitedFileHandler;
        private readonly string _filePath = @"C:\PATaxAffidavitMailingFinalValidAddresses.csv";

        public XVerifyMailingAddressValidFileProcessor(
            IDatabaseCommanderFactory databaseCommandFactory,
            IDelimitedFileHandler delimitedFileHandler)
        {
            _databaseCommander = databaseCommandFactory.Create(DefaultConnection);
            _delimitedFileHandler = delimitedFileHandler;
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            DataTable dataTable = _delimitedFileHandler.GetFileAsDataTable(_filePath);

            var errorList = new List<string>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                string inputAddress = dataRow.GetStringOrNull("Address");
                string inputCity = dataRow.GetStringOrNull("City");
                string inputState = dataRow.GetStringOrNull("State");
                string inputZip = dataRow.GetStringOrNull("Zip");

                string validatedAddress = dataRow.GetStringOrNull("xv_address1");
                string validatedCity = dataRow.GetStringOrNull("xv_city");
                string validatedState = dataRow.GetStringOrNull("xv_state");
                string validatedZip = dataRow.GetStringOrNull("xv_zip");
                string validatedZipLastFour = dataRow.GetStringOrNull("plus4");

                if (!string.IsNullOrEmpty(validatedZip) && !string.IsNullOrEmpty(validatedZipLastFour))
                {
                    validatedZip = $"{validatedZip}-{validatedZipLastFour}";
                }

                if (inputAddress.Split(',').Count() > 2)
                {
                    errorList.Add($"Could not resolve inputAddress of {inputAddress} due to too many commas");
                }
                else
                {
                    string inputAddressLine2 = string.Empty;

                    string[] addressArray = inputAddress.Split(',');

                    if (addressArray.Count() == 2)
                    {
                        inputAddress = addressArray[0].Trim();
                        inputAddressLine2 = addressArray[1].Trim();
                    }

                    string sql = GetSql(inputAddressLine2);

                    SqlNonQueryResult result = await _databaseCommander.BuildCommand()
                        .ForSqlNonQuery(sql)
                        .AddInputParameter("inputAddress", inputAddress)
                        .AddInputParameter("inputAddressLine2", inputAddressLine2)
                        .AddInputParameter("inputCity", inputCity)
                        .AddInputParameter("inputState", inputState)
                        .AddInputParameter("validatedAddress", validatedAddress)
                        .AddInputParameter("validatedCity", validatedCity)
                        .AddInputParameter("validatedState", validatedState)
                        .AddInputParameter("validatedZip", validatedZip)
                        .ExecuteAsync(cancellationToken);

                    if (result.RowCountAffected == 0)
                    {
                        errorList.Add($"The following address was not found: inputAddress: {inputAddress}, inputAddressLine2: {inputAddressLine2}, inputCity: {inputCity}, inputState: {inputState}, inputZip: {inputZip}");
                    }
                }
            }

            if (errorList.Any())
            {
                throw new Exception("Could not process: " + string.Join(Environment.NewLine, errorList));
            }
        }

        private string GetSql(string inputAddressLine2)
        {
            string sql =
@"UPDATE [dbo].[TaxAffidavit]
SET isUseMailingAddressAlt = 1
  , mailingAddressAltLine1 = @validatedAddress
  , mailingAddressAltCity = @validatedCity
  , mailingAddressAltState = @validatedState
  , mailingAddressAltPostalCode = @validatedZip
  , modifyUser = 'UpdateMailingAddressAltFromXVerify'
  , modifyTS = CURRENT_TIMESTAMP
WHERE 1 = 1
  AND mailingAddressLine1 = @inputAddress
  AND mailingAddressCity = @inputCity
  AND mailingAddressState = @inputState";

            if (!string.IsNullOrEmpty(inputAddressLine2))
            {
                sql += "  AND mailingAddressLine2 = @inputAddressLine2";
            }

            return sql;
        }
    }
}
