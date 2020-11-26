using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class EventEntity : Core.Framework.IReadOnly
    {
        public long EventId { get; set; }
        public long CustomerId { get; set; }
        public int EventTypeId { get; set; }
        public DateTime EventDate { get; set; }
        public string Notes { get; set; }
        public bool IsSuppressed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual EventTypeEntity EventType { get; set; }
    }
}
