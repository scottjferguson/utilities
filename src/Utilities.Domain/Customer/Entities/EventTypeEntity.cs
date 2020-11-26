using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class EventTypeEntity : Core.Framework.IAuditable
    {
        public EventTypeEntity()
        {
            Events = new HashSet<EventEntity>();
        }

        public int EventTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<EventEntity> Events { get; set; }
    }
}
