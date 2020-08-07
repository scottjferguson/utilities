using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PhoneNumberHistory
    {
        public long PhoneNumberHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long PhoneNumberId { get; set; }
        public string PhoneNumberGlobalId { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberNumeric { get; set; }
        public string CountryCode { get; set; }
        public bool? IsOnDnclist { get; set; }
        public bool? IsValid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
