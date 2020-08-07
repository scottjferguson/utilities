﻿using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class EmailAddress
    {
        public EmailAddress()
        {
            PersonToEmailAddress = new HashSet<PersonToEmailAddress>();
        }

        public long EmailAddressId { get; set; }
        public string EmailAddressGlobalId { get; set; }
        public int EmailAddressTypeId { get; set; }
        public string EmailAddress1 { get; set; }
        public bool? IsValid { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual EmailAddressType EmailAddressType { get; set; }
        public virtual ICollection<PersonToEmailAddress> PersonToEmailAddress { get; set; }
    }
}
