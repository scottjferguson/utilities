using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerHistoryEntity : Core.Framework.IAuditable
    {
        public long CustomerHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string HistoryCreatedBy { get; set; }
        public DateTime HistoryCreatedDate { get; set; }
        public long CustomerId { get; set; }
        public string CustomerGlobalId { get; set; }
        public long UsernameId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public int? CustomerSourceId { get; set; }
        public string BrandCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string BusinessName { get; set; }
        public string AvatarUrl { get; set; }
        public string ExternalReferenceId { get; set; }
        public bool? IsViewedWalkthrough { get; set; }
        public bool? IsSoftDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
