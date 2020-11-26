using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class AddressHistoryEntity : Core.Framework.IAuditable
    {
        public long AddressHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long AddressId { get; set; }
        public long CustomerId { get; set; }
        public int AddressTypeId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string PostalCode { get; set; }
        public string PostalCodeLastFour { get; set; }
        public string County { get; set; }
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }
        public bool? IsValid { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsPrimaryBilling { get; set; }
        public bool IsPrimaryShipping { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
