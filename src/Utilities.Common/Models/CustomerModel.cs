namespace Utilities.Common.Models
{
    public class CustomerModel : PersonModel
    {
        public CustomerModel() { }

        public CustomerModel(string firstName, string lastName, string emailAddress, string phoneNumber, string addressLine1, string city, string stateProv, string postalCode, string status = "Active")
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Status = status;
            BillingAddress = new AddressModel
            {
                Line1 = addressLine1,
                City = city,
                StateProv = stateProv,
                PostalCode = postalCode
            };
        }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public AddressModel BillingAddress { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
