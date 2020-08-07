using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerAttributeType
    {
        public CustomerAttributeType()
        {
            CustomerAttribute = new HashSet<CustomerAttribute>();
        }

        public int CustomerAttributeTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsValueEncrypted { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CustomerAttribute> CustomerAttribute { get; set; }
    }
}
