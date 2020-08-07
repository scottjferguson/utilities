namespace Utilities.Common.Models
{
    public class AddressModel
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string StateProv { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }

        public string DisplayLine1
        {
            get
            {
                if (string.IsNullOrEmpty(Line1))
                {
                    return "Not on file";
                }

                return $"{Line1} {Line2}";
            }
        }

        public string DisplayLine2
        {
            get
            {
                if (string.IsNullOrEmpty(City))
                {
                    return $"{StateProv} {PostalCode}";
                }

                return $"{City}, {StateProv} {PostalCode}";
            }
        }
    }
}
