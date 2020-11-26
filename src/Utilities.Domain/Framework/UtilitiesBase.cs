using System;

namespace Utilities.Domain.Framework
{
    public class UtilitiesBase
    {
        public readonly string CreatedBy = "scott@guroosolutions.com";
        public readonly DateTime CreatedDate = DateTime.UtcNow;
        public readonly string DefaultConnection = Constants.Configuration.ConnectionString.DefaultConnection;
        public readonly string FulfillmentStorageAccount = Constants.Configuration.ConnectionString.FulfillmentStorageAccount;
    }
}
