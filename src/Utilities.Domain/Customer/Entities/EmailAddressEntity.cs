using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class EmailAddressEntity : Core.Framework.IAuditable
    {
        public long EmailAddressId { get; set; }
        public long CustomerId { get; set; }
        public int EmailAddressTypeId { get; set; }
        public string EmailAddress1 { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsPrimary { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual EmailAddressTypeEntity EmailAddressType { get; set; }
    }
}
