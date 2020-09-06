using Core.Providers;
using Microsoft.EntityFrameworkCore;
using Utilities.Domain.Customer.Entities;

namespace Utilities.Domain.Customer.Context
{
    public class CustomerDbContext : CustomerContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public CustomerDbContext(IConnectionStringProvider connectionStringProvider)
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
