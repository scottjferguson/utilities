using System;

namespace Utilities.Domain.Customer.Entities
{
    public partial class AddressEntity : Core.Framework.IAuditable
    {
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
        public bool? IsActive { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsPhysical { get; set; }
        public bool IsPrimaryBilling { get; set; }
        public bool IsPrimaryShipping { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual AddressTypeEntity AddressType { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
