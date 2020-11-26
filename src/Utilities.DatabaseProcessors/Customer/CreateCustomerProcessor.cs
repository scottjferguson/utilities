using ECommerce.Services;
using Processor;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain.Customer.Context;
using Utilities.Domain.Customer.Entities;
using Utilities.Domain.Framework;

namespace Utilities.DatabaseProcessors.Customer
{
    [Processor]
    public class CreateCustomerProcessor : ProcessorBase
    {
        private readonly CustomerDbContext _dbContext;
        private readonly IConfirmationNumberService _confirmationNumberService;

        public CreateCustomerProcessor(
            CustomerDbContext dbContext,
            IConfirmationNumberService confirmationNumberService)
        {
            _dbContext = dbContext;
            _confirmationNumberService = confirmationNumberService;
        }
        
        public override async Task ProcessAsync(CancellationToken cancellationToken)
        {
            foreach (ECommerce.Customer customer in GetCustomersToCreate())
            {
                var customerEntity = MapToEntity(customer);

                await _dbContext.Customer.AddAsync(customerEntity, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        private List<ECommerce.Customer> GetCustomersToCreate()
        {
            return new List<ECommerce.Customer>
            {
                CreateCustomer("Scott", "Ferguson", "scott@guroosolutions.com", "555-555-5555", "1 Main St.", "Austin", "TX", "78701", "72C931AF-53BC-409F-8EFA-21198FB724CD"),
                CreateCustomer("Guroo", "Test", "scott.ferguson82@gmail.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77001", "2B53FCBF-6FC1-467F-BC51-7FB9943E0110"),
                CreateCustomer("Pradeep", "Tiwari", "pradeep@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77047"),
                CreateCustomer("Andy", "Gregory", "andy@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77301"),
                CreateCustomer("Ed", "Janson", "ed@guroosolutions.com", "555-555-5555", "1 Main St.", "Houston", "TX", "77001")
            };
        }

        private ECommerce.Customer CreateCustomer(string firstName, string lastName, string emailAddress, string phoneNumber, string addressLine1, string city, string stateProv, string postalCode, string customerGlobalId = null, string status = "Active")
        {
            return new ECommerce.Customer
            {
                FirstName = firstName,
                LastName = lastName,
                CustomerStatus = status,
                ExternalReferenceId = customerGlobalId,
                Addresses = new List<ECommerce.Address>
                {
                    new ECommerce.Address
                    {
                        Line1 = addressLine1,
                        City = city,
                        StateProv = stateProv,
                        PostalCode = postalCode
                    }
                },
                EmailAddresses = new List<ECommerce.EmailAddress>
                {
                    new ECommerce.EmailAddress
                    {
                        EmailAddressValue = emailAddress
                    }
                },
                PhoneNumbers = new List<ECommerce.PhoneNumber>
                {
                    new ECommerce.PhoneNumber
                    {
                        PhoneNumberValue = phoneNumber
                    }
                }
            };
        }

        private Domain.Customer.Entities.Customer MapToEntity(ECommerce.Customer customer)
        {
            string customerNumber = _confirmationNumberService.GenerateConfirmationNumber(10, "100");

            return new Domain.Customer.Entities.Customer
            {
                CustomerStatusId = 1,
                CustomerTypeId = 1,
                BrandCode = "GUR",
                CustomerNumber = customerNumber,
                CustomerGlobalId = customer.ExternalReferenceId,
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
                                Line1 = customer.Addresses[0].Line1,
                                City = customer.Addresses[0].City,
                                StateProv = customer.Addresses[0].StateProv,
                                PostalCode = customer.Addresses[0].PostalCode,
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
                                EmailAddress1 = customer.EmailAddresses[0].EmailAddressValue,
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
                                PhoneNumber1 = customer.PhoneNumbers[0].PhoneNumberValue,
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
