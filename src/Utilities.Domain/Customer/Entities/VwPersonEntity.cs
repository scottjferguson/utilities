using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class VwPersonEntity
    {
        public long PersonId { get; set; }
        public string PersonGlobalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
