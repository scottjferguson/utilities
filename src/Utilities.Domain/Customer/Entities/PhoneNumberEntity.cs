using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PhoneNumberEntity : Core.Framework.IAuditable
    {
        public long PhoneNumberId { get; set; }
        public long CustomerId { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumberNumeric { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsOnDnclist { get; set; }
        public bool? IsPrimary { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual PhoneNumberTypeEntity PhoneNumberType { get; set; }
    }
}
