using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class UsernameEntity : Core.Framework.IReadOnly
    {
        public UsernameEntity()
        {
            Customers = new HashSet<CustomerEntity>();
        }

        public long UsernameId { get; set; }
        public string Username1 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CustomerEntity> Customers { get; set; }
    }
}
