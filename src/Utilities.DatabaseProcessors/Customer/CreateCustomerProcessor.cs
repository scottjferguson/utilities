using Core.Application;
using Core.Plugins.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;
using Utilities.Common.Models;
using Utilities.Common.Providers;
using Utilities.Domain.Customer.Context;
using Utilities.Domain.Customer.Entities;

namespace Utilities.DatabaseProcessors.Customer
{
    [Processor(Name = "CreateCustomer")]
    public class CreateCustomerProcessor : ProcessorBase
    {
        private readonly UtilitiesCustomerContext _dbContext;
        private readonly RandomCodeProvider _randomCodeProvider;

        public CreateCustomerProcessor(
            UtilitiesCustomerContext dbContext,
            RandomCodeProvider randomCodeProvider)
        {
            _dbContext = dbContext;
            _randomCodeProvider = randomCodeProvider;
        }

        public override async Task ProcessAsync(CancellationToken cancellationToken)
        {
            foreach (CustomerModel customerModel in GetCustomersToCreate())
            {
                var customer = MapToEntity(customerModel);

                await _dbContext.Customer.AddAsync(customer, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private List<CustomerModel> GetCustomersToCreate()
        {
            return new List<CustomerModel>
            {
                new CustomerModel("Scott", "Ferguson", "scott@guroosolutions.com", "555-555-5555", "1 Main St.", "Austin", "TX", "78701"),
                new CustomerModel("Pradeep", "Tiwari", "pradeep@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77047"),
                new CustomerModel("Andy", "Gregory", "andy@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77301"),
                new CustomerModel("Ed", "Janson", "ed@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77001"),
                new CustomerModel("Manicka", "Manikandan", "manicka.manikandan@publicissapient.com", "555-555-5555", "1 Main St.", "Arlington", "VA", "22201")
            };
        }

        private Domain.Customer.Entities.Customer MapToEntity(CustomerModel customer)
        {
            string customerNumber = _randomCodeProvider.GenerateConfirmationNumber(10, "000");

            return new Domain.Customer.Entities.Customer
            {
                CustomerStatusId = 1,
                CustomerTypeId = 1,
                CustomerNumber = customerNumber,
                JoinDate = CreatedDate,
                CreatedBy = CreatedBy,
                CreatedDate = CreatedDate,
                Person = new Person
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CreatedBy = CreatedBy,
                    CreatedDate = CreatedDate,
                    PersonToAddress = new List<PersonToAddress>
                    {
                        new PersonToAddress
                        {
                            Address = new Address
                            {
                                AddressTypeId = 2,
                                Line1 = customer.BillingAddress.Line1,
                                City = customer.BillingAddress.City,
                                StateProv = customer.BillingAddress.StateProv,
                                PostalCode = customer.BillingAddress.PostalCode,
                                CountryCode = "US",
                                CreatedBy = CreatedBy,
                                CreatedDate = CreatedDate
                            },
                            IsPrimaryBilling = true,
                            CreatedBy = CreatedBy,
                            CreatedDate = CreatedDate
                        }
                    },
                    PersonToEmailAddress = new List<PersonToEmailAddress>
                    {
                        new PersonToEmailAddress
                        {
                            EmailAddress = new EmailAddress
                            {
                                EmailAddressTypeId = 1,
                                EmailAddress1 = customer.EmailAddress,
                                CreatedBy = CreatedBy,
                                CreatedDate = CreatedDate
                            },
                            CreatedBy = CreatedBy,
                            CreatedDate = CreatedDate
                        }
                    },
                    PersonToPhoneNumber = new List<PersonToPhoneNumber>
                    {
                        new PersonToPhoneNumber
                        {
                            PhoneNumber = new PhoneNumber
                            {
                                PhoneNumberTypeId = 1,
                                PhoneNumber1 = customer.PhoneNumber,
                                PhoneNumberNumeric = customer.PhoneNumber.ToNumeric(),
                                CreatedBy = CreatedBy,
                                CreatedDate = CreatedDate
                            },
                            CreatedBy = CreatedBy,
                            CreatedDate = CreatedDate
                        }
                    }
                }
            };
        }
    }
}
