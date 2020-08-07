using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PersonToEmailAddress
    {
        public long PersonToEmailAddressId { get; set; }
        public long PersonId { get; set; }
        public long EmailAddressId { get; set; }
        public bool IsPrimary { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual EmailAddress EmailAddress { get; set; }
        public virtual Person Person { get; set; }
    }
}
