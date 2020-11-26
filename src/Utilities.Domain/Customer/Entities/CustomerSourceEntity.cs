using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerSourceEntity : Core.Framework.IAuditable
    {
        public CustomerSourceEntity()
        {
            Customers = new HashSet<CustomerEntity>();
        }

        public int CustomerSourceId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }
    }
}
