using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class Person
    {
        public Person()
        {
            Customer = new HashSet<Customer>();
            PersonToAddress = new HashSet<PersonToAddress>();
            PersonToEmailAddress = new HashSet<PersonToEmailAddress>();
            PersonToPhoneNumber = new HashSet<PersonToPhoneNumber>();
        }

        public long PersonId { get; set; }
        public string PersonGlobalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<PersonToAddress> PersonToAddress { get; set; }
        public virtual ICollection<PersonToEmailAddress> PersonToEmailAddress { get; set; }
        public virtual ICollection<PersonToPhoneNumber> PersonToPhoneNumber { get; set; }
    }
}
