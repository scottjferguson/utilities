using Core.Providers;
using Microsoft.EntityFrameworkCore;
using Utilities.Domain.Customer.Entities;

namespace Utilities.Domain.Customer.Context
{
    public class UtilitiesCustomerContext : CustomerContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public UtilitiesCustomerContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionStringProvider.Get("CustomerDatabase"));
            }
        }
    }
}
