using System;

namespace Utilities.Common
{
    public class UtilitiesBase
    {
        public readonly string CreatedBy = "sferguson";
        public readonly DateTime CreatedDate = DateTime.UtcNow;
        public readonly string DefaultConnection = Constants.Configuration.ConnectionString.DefaultConnection;
        public readonly string FulfillmentStorageAccount = Constants.Configuration.ConnectionString.FulfillmentStorageAccount;
    }
}
