using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PhoneNumberTypeEntity : Core.Framework.IAuditable
    {
        public PhoneNumberTypeEntity()
        {
            PhoneNumbers = new HashSet<PhoneNumberEntity>();
        }

        public int PhoneNumberTypeId { get; set; }
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

        public virtual ICollection<PhoneNumberEntity> PhoneNumbers { get; set; }
    }
}
