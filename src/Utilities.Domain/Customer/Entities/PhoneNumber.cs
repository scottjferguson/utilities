using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PhoneNumber
    {
        public PhoneNumber()
        {
            PersonToPhoneNumber = new HashSet<PersonToPhoneNumber>();
        }

        public long PhoneNumberId { get; set; }
        public string PhoneNumberGlobalId { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumberNumeric { get; set; }
        public string CountryCode { get; set; }
        public bool? IsOnDnclist { get; set; }
        public bool? IsValid { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }
        public virtual ICollection<PersonToPhoneNumber> PersonToPhoneNumber { get; set; }
    }
}
