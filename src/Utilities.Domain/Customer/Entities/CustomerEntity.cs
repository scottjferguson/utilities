using System;
using System.Collections.Generic;
using Utilities.Domain.Customer.Entities;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerEntity : Core.Framework.IAuditable
    {
        public CustomerEntity()
        {
            Addresses = new HashSet<AddressEntity>();
            Attributes = new HashSet<AttributeEntity>();
            EmailAddresses = new HashSet<EmailAddressEntity>();
            Events = new HashSet<EventEntity>();
            Notes = new HashSet<NoteEntity>();
            PhoneNumbers = new HashSet<PhoneNumberEntity>();
            Searches = new HashSet<SearchEntity>();
        }

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
        public bool? IsActive { get; set; }
        public string BusinessName { get; set; }
        public string AvatarUrl { get; set; }
        public string ExternalReferenceId { get; set; }
        public bool IsViewedWalkthrough { get; set; }
        public bool IsSoftDelete { get; set; }
        public byte[] RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CustomerSourceEntity CustomerSource { get; set; }
        public virtual CustomerStatusEntity CustomerStatus { get; set; }
        public virtual CustomerTypeEntity CustomerType { get; set; }
        public virtual UsernameEntity Username { get; set; }
        public virtual ICollection<AddressEntity> Addresses { get; set; }
        public virtual ICollection<AttributeEntity> Attributes { get; set; }
        public virtual ICollection<EmailAddressEntity> EmailAddresses { get; set; }
        public virtual ICollection<EventEntity> Events { get; set; }
        public virtual ICollection<NoteEntity> Notes { get; set; }
        public virtual ICollection<PhoneNumberEntity> PhoneNumbers { get; set; }
        public virtual ICollection<SearchEntity> Searches { get; set; }
    }
}
