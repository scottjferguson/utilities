using System;
using System.Collections.Generic;

namespace Utilities.Domain.Customer.Entities
{
    public partial class VwCustomerDetailEntity
    {
        public long CustomerId { get; set; }
        public string CustomerGlobalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerStatus { get; set; }
        public string CustomerType { get; set; }
        public DateTime JoinDate { get; set; }
        public string CustomerNumber { get; set; }
        public string ExternalReferenceId { get; set; }
        public bool IsActive { get; set; }
        public bool IsPersonActive { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string StateProv { get; set; }
        public string PostalCode { get; set; }
        public string County { get; set; }
        public string CountryCode { get; set; }
        public bool? IsAddressValid { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsPrimaryBilling { get; set; }
        public bool IsPrimaryShipping { get; set; }
        public bool IsAddressActive { get; set; }
        public string EmailAddressType { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsEmailAddressValid { get; set; }
        public bool IsEmailAddressPrimary { get; set; }
        public bool IsEmailAddressActive { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberNumeric { get; set; }
        public bool? IsOnDnclist { get; set; }
        public bool? IsPhoneNumberValid { get; set; }
        public bool IsPhoneNumberPrimary { get; set; }
        public bool IsPhoneNumberActive { get; set; }
        public string PersonGlobalId { get; set; }
        public long PersonId { get; set; }
        public int CustomerStatusId { get; set; }
        public int CustomerTypeId { get; set; }
        public long AddressId { get; set; }
        public long EmailAddressId { get; set; }
        public long PhoneNumberId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
