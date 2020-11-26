using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class VwCustomerEntity
    {
        public long CustomerId { get; set; }
        public string CustomerGlobalId { get; set; }
        public string CustomerStatus { get; set; }
        public string CustomerType { get; set; }
        public string BrandCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string ExternalReferenceId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
