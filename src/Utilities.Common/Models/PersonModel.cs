namespace Utilities.Common.Models
{
    public class PersonModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddressBilling { get; set; }

        public string EmailAddressAccount { get; set; }

        public string MailingAddressLine1 { get; set; }

        public string Name => $"FirstName: {FirstName} LastName: {LastName}";
    }
}
