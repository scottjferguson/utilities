using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class AttributeTypeEntity : Core.Framework.IAuditable
    {
        public AttributeTypeEntity()
        {
            Attributes = new HashSet<AttributeEntity>();
        }

        public int AttributeTypeId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsValueEncrypted { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<AttributeEntity> Attributes { get; set; }
    }
}
