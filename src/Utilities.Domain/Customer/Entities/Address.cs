using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class Address
    {
        public Address()
        {
            PersonToAddress = new HashSet<PersonToAddress>();
        }

        public long AddressId { get; set; }
        public string AddressGlobalId { get; set; }
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
        public bool? IsValid { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual ICollection<PersonToAddress> PersonToAddress { get; set; }
    }
}
