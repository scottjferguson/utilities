using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class AttributeHistoryEntity : Core.Framework.IAuditable
    {
        public long AttributeHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long AttributeId { get; set; }
        public long CustomerId { get; set; }
        public int AttributeTypeId { get; set; }
        public string AttributeValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
