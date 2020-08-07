using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PersonToAddress
    {
        public long PersonToAddressId { get; set; }
        public long PersonId { get; set; }
        public long AddressId { get; set; }
        public bool? IsPhysical { get; set; }
        public bool IsPrimaryBilling { get; set; }
        public bool IsPrimaryShipping { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
