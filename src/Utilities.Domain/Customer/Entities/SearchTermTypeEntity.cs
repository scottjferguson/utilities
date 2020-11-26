using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class SearchTermTypeEntity : Core.Framework.IAuditable
    {
        public SearchTermTypeEntity()
        {
            Searches = new HashSet<SearchEntity>();
        }

        public int SearchTermTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<SearchEntity> Searches { get; set; }
    }
}
