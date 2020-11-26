using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class AttributeEntity : Core.Framework.IAuditable
    {
        public long AttributeId { get; set; }
        public long CustomerId { get; set; }
        public int AttributeTypeId { get; set; }
        public string AttributeValue { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual AttributeTypeEntity AttributeType { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
