using Core.Plugins.EntityFramework.Auditor;
using Core.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Domain.Customer.Entities;

namespace Utilities.Domain.Customer.Context
{
    public class CustomerDbContext : CustomerContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly IEntityAuditor _entityAuditor;

        public CustomerDbContext(IConnectionStringProvider connectionStringProvider, IEntityAuditor entityAuditor)
        {
            _connectionStringProvider = connectionStringProvider;
            _entityAuditor = entityAuditor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionStringProvider.Get());
            }
        }

        public override int SaveChanges()
        {
            _entityAuditor.Audit(ChangeTracker.Entries());

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _entityAuditor.Audit(ChangeTracker.Entries());

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (Type _entityType in _entityTypes)
            {
                modelBuilder.Entity(_entityType, entity =>
                {
                    entity.ToTable(_entityType.Name.Replace("Entity", ""));
                });
            }

            // Not sure why EF is not generating the HasKey() API directly on the context. Until that's figured out, define the keys here where it's stable
            modelBuilder.Entity<AddressEntity>(entity =>
            {
                entity.HasKey(e => e.AddressId).IsClustered();
            });

            modelBuilder.Entity<AddressHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.AddressHistoryId).IsClustered();
            });

            modelBuilder.Entity<AddressTypeEntity>(entity =>
            {
                entity.HasKey(e => e.AddressTypeId).IsClustered();
            });

            modelBuilder.Entity<AttributeEntity>(entity =>
            {
                entity.HasKey(e => e.AttributeId).IsClustered();
            });

            modelBuilder.Entity<AttributeHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.AttributeHistoryId).IsClustered();
            });

            modelBuilder.Entity<AttributeTypeEntity>(entity =>
            {
                entity.HasKey(e => e.AttributeTypeId).IsClustered();
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.CustomerId).IsClustered();
            });

            modelBuilder.Entity<CustomerHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.CustomerHistoryId).IsClustered();
            });

            modelBuilder.Entity<CustomerStatusEntity>(entity =>
            {
                entity.HasKey(e => e.CustomerStatusId).IsClustered();
            });

            modelBuilder.Entity<CustomerTypeEntity>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId).IsClustered();
            });

            modelBuilder.Entity<CustomerSourceEntity>(entity =>
            {
                entity.HasKey(e => e.CustomerSourceId).IsClustered();
            });

            modelBuilder.Entity<EmailAddressEntity>(entity =>
            {
                entity.HasKey(e => e.EmailAddressId).IsClustered();
            });

            modelBuilder.Entity<EmailAddressHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.EmailAddressHistoryId).IsClustered();
            });

            modelBuilder.Entity<EmailAddressTypeEntity>(entity =>
            {
                entity.HasKey(e => e.EmailAddressTypeId).IsClustered();
            });

            modelBuilder.Entity<EventEntity>(entity =>
            {
                entity.HasKey(e => e.EventId).IsClustered();
            });

            modelBuilder.Entity<EventTypeEntity>(entity =>
            {
                entity.HasKey(e => e.EventTypeId).IsClustered();
            });

            modelBuilder.Entity<NoteEntity>(entity =>
            {
                entity.HasKey(e => e.NoteId).IsClustered();
            });

            modelBuilder.Entity<NoteTypeEntity>(entity =>
            {
                entity.HasKey(e => e.NoteTypeId).IsClustered();
            });

            modelBuilder.Entity<PhoneNumberEntity>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberId).IsClustered();
            });

            modelBuilder.Entity<PhoneNumberHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberHistoryId).IsClustered();
            });

            modelBuilder.Entity<PhoneNumberTypeEntity>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberTypeId).IsClustered();
            });

            modelBuilder.Entity<SearchTermTypeEntity>(entity =>
            {
                entity.HasKey(e => e.SearchTermTypeId).IsClustered();
            });
        }

        private readonly List<Type> _entityTypes = new List<Type>
        {
            typeof(AddressEntity),
            typeof(AddressHistoryEntity),
            typeof(AddressTypeEntity),
            typeof(AttributeEntity),
            typeof(AttributeHistoryEntity),
            typeof(AttributeTypeEntity),
            typeof(CustomerEntity),
            typeof(CustomerHistoryEntity),
            typeof(CustomerStatusEntity),
            typeof(CustomerSourceEntity),
            typeof(CustomerTypeEntity),
            typeof(EmailAddressEntity),
            typeof(EmailAddressHistoryEntity),
            typeof(EmailAddressTypeEntity),
            typeof(EventEntity),
            typeof(EventTypeEntity),
            typeof(NoteEntity),
            typeof(NoteTypeEntity),
            typeof(PhoneNumberEntity),
            typeof(PhoneNumberHistoryEntity),
            typeof(PhoneNumberTypeEntity),
            typeof(SearchEntity),
            typeof(SearchTermTypeEntity),
            typeof(UsernameEntity)
        };
    }
}
