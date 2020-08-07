using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerAttributeHistory
    {
        public long CustomerAttributeHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long CustomerAttributeId { get; set; }
        public long CustomerId { get; set; }
        public int CustomerAttributeTypeId { get; set; }
        public string AttributeValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
