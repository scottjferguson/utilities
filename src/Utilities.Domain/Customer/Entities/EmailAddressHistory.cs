﻿using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class EmailAddressHistory
    {
        public long EmailAddressHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long EmailAddressId { get; set; }
        public string EmailAddressGlobalId { get; set; }
        public int EmailAddressTypeId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsValid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
