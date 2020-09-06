using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerHistory
    {
        public long CustomerHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long CustomerId { get; set; }
        public string CustomerGlobalId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public string BrandCode { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string ExternalReferenceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
