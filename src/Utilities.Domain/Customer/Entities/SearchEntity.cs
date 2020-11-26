using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class SearchEntity : Core.Framework.IReadOnly
    {
        public long SearchId { get; set; }
        public long CustomerId { get; set; }
        public int SearchTermTypeId { get; set; }
        public string SearchTerm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual SearchTermTypeEntity SearchTermType { get; set; }
    }
}
