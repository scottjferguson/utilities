using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class PersonHistory
    {
        public long PersonHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long PersonId { get; set; }
        public string PersonGlobalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
