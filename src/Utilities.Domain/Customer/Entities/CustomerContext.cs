using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Utilities.Domain.Customer.Entities
{
    public partial class CustomerContext : DbContext
    {
        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressHistory> AddressHistory { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAttribute> CustomerAttribute { get; set; }
        public virtual DbSet<CustomerAttributeHistory> CustomerAttributeHistory { get; set; }
        public virtual DbSet<CustomerAttributeType> CustomerAttributeType { get; set; }
        public virtual DbSet<CustomerHistory> CustomerHistory { get; set; }
        public virtual DbSet<CustomerNote> CustomerNote { get; set; }
        public virtual DbSet<CustomerNoteType> CustomerNoteType { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatus { get; set; }
        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<EmailAddress> EmailAddress { get; set; }
        public virtual DbSet<EmailAddressHistory> EmailAddressHistory { get; set; }
        public virtual DbSet<EmailAddressType> EmailAddressType { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonHistory> PersonHistory { get; set; }
        public virtual DbSet<PersonToAddress> PersonToAddress { get; set; }
        public virtual DbSet<PersonToEmailAddress> PersonToEmailAddress { get; set; }
        public virtual DbSet<PersonToPhoneNumber> PersonToPhoneNumber { get; set; }
        public virtual DbSet<PersonType> PersonType { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; }
        public virtual DbSet<PhoneNumberHistory> PhoneNumberHistory { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.AddressGlobalId)
                    .HasName("IX__Address__AddressGlobalID");

                entity.HasIndex(e => e.City)
                    .HasName("IX__Address__City");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("IX__Address__CountryCode");

                entity.HasIndex(e => e.Line1)
                    .HasName("IX__Address__Line1");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("IX__Address__PostalCode");

                entity.HasIndex(e => e.StateProv)
                    .HasName("IX__Address__StateProv");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressGlobalId)
                    .IsRequired()
                    .HasColumnName("AddressGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Line1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Line2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Line3)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCodeLastFour)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.StateProv)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__AddressTypeID");
            });

            modelBuilder.Entity<AddressHistory>(entity =>
            {
                entity.Property(e => e.AddressHistoryId).HasColumnName("AddressHistoryID");

                entity.Property(e => e.AddressGlobalId)
                    .IsRequired()
                    .HasColumnName("AddressGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Line1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Line2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Line3)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCodeLastFour)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.StateProv)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX__AddressType__Name");

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.CustomerGlobalId)
                    .HasName("IX__Customer__CustomerGlobalID");

                entity.HasIndex(e => e.CustomerNumber)
                    .HasName("IX__Customer__CustomerNumber");

                entity.HasIndex(e => e.ExternalReferenceId)
                    .HasName("IX__Customer__ExternalReferenceID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CustomerGlobalId)
                    .IsRequired()
                    .HasColumnName("CustomerGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CustomerNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerStatusId).HasColumnName("CustomerStatusID");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");

                entity.Property(e => e.ExternalReferenceId)
                    .HasColumnName("ExternalReferenceID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CustomerStatus)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.CustomerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__CustomerStatusID");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__CustomerTypeID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__PersonID");
            });

            modelBuilder.Entity<CustomerAttribute>(entity =>
            {
                entity.Property(e => e.CustomerAttributeId).HasColumnName("CustomerAttributeID");

                entity.Property(e => e.AttributeValue)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CustomerAttributeTypeId).HasColumnName("CustomerAttributeTypeID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CustomerAttributeType)
                    .WithMany(p => p.CustomerAttribute)
                    .HasForeignKey(d => d.CustomerAttributeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerAttribute__CustomerAttributeTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAttribute)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerAttribute__CustomerID");
            });

            modelBuilder.Entity<CustomerAttributeHistory>(entity =>
            {
                entity.Property(e => e.CustomerAttributeHistoryId).HasColumnName("CustomerAttributeHistoryID");

                entity.Property(e => e.AttributeValue)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerAttributeId).HasColumnName("CustomerAttributeID");

                entity.Property(e => e.CustomerAttributeTypeId).HasColumnName("CustomerAttributeTypeID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerAttributeType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX__CustomerEventType__Name");

                entity.Property(e => e.CustomerAttributeTypeId).HasColumnName("CustomerAttributeTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerHistory>(entity =>
            {
                entity.Property(e => e.CustomerHistoryId).HasColumnName("CustomerHistoryID");

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerGlobalId)
                    .IsRequired()
                    .HasColumnName("CustomerGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerStatusId).HasColumnName("CustomerStatusID");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");

                entity.Property(e => e.ExternalReferenceId)
                    .HasColumnName("ExternalReferenceID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerNote>(entity =>
            {
                entity.Property(e => e.CustomerNoteId).HasColumnName("CustomerNoteID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerNoteTypeId).HasColumnName("CustomerNoteTypeID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerNote)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerNote__CustomerID");

                entity.HasOne(d => d.CustomerNoteType)
                    .WithMany(p => p.CustomerNote)
                    .HasForeignKey(d => d.CustomerNoteTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerNote__CustomerNoteTypeID");
            });

            modelBuilder.Entity<CustomerNoteType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX__CustomerNote__Name");

                entity.Property(e => e.CustomerNoteTypeId).HasColumnName("CustomerNoteTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX__CustomerStatus__Code");

                entity.HasIndex(e => e.Name)
                    .HasName("IX__CustomerStatus__Name");

                entity.HasIndex(e => e.SubStatusName)
                    .HasName("IX__CustomerStatus__SubStatusName");

                entity.HasIndex(e => new { e.Name, e.SubStatusName })
                    .HasName("UC__CustomerStatus__Name__SubStatusName")
                    .IsUnique();

                entity.Property(e => e.CustomerStatusId).HasColumnName("CustomerStatusID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubStatusName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX__CustomerType__Code");

                entity.HasIndex(e => e.Name)
                    .HasName("IX__CustomerType__Name");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress1)
                    .HasName("IX__EmailAddress__EmailAddress");

                entity.HasIndex(e => e.EmailAddressGlobalId)
                    .HasName("IX__EmailAddress__EmailAddressGlobalID");

                entity.Property(e => e.EmailAddressId).HasColumnName("EmailAddressID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EmailAddress1)
                    .IsRequired()
                    .HasColumnName("EmailAddress")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddressGlobalId)
                    .IsRequired()
                    .HasColumnName("EmailAddressGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.EmailAddressTypeId).HasColumnName("EmailAddressTypeID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.EmailAddressType)
                    .WithMany(p => p.EmailAddress)
                    .HasForeignKey(d => d.EmailAddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmailAddress__EmailAddressTypeID");
            });

            modelBuilder.Entity<EmailAddressHistory>(entity =>
            {
                entity.Property(e => e.EmailAddressHistoryId).HasColumnName("EmailAddressHistoryID");

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddressGlobalId)
                    .IsRequired()
                    .HasColumnName("EmailAddressGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddressId).HasColumnName("EmailAddressID");

                entity.Property(e => e.EmailAddressTypeId).HasColumnName("EmailAddressTypeID");

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailAddressType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX__EmailAddressType__Name");

                entity.Property(e => e.EmailAddressTypeId).HasColumnName("EmailAddressTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.FirstName)
                    .HasName("IX__Person__FirstName");

                entity.HasIndex(e => e.LastName)
                    .HasName("IX__Person__LastName");

                entity.HasIndex(e => e.PersonGlobalId)
                    .HasName("IX__Person__PersonGlobalID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonGlobalId)
                    .IsRequired()
                    .HasColumnName("PersonGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PersonHistory>(entity =>
            {
                entity.Property(e => e.PersonHistoryId).HasColumnName("PersonHistoryID");

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonGlobalId)
                    .IsRequired()
                    .HasColumnName("PersonGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");
            });

            modelBuilder.Entity<PersonToAddress>(entity =>
            {
                entity.Property(e => e.PersonToAddressId).HasColumnName("PersonToAddressID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsPhysical)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonToAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToAddress__AddressID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonToAddress)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToAddress__PersonID");
            });

            modelBuilder.Entity<PersonToEmailAddress>(entity =>
            {
                entity.Property(e => e.PersonToEmailAddressId).HasColumnName("PersonToEmailAddressID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.EmailAddressId).HasColumnName("EmailAddressID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.EmailAddress)
                    .WithMany(p => p.PersonToEmailAddress)
                    .HasForeignKey(d => d.EmailAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToEmailAddress__EmailAddressID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonToEmailAddress)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToEmailAddress__PersonID");
            });

            modelBuilder.Entity<PersonToPhoneNumber>(entity =>
            {
                entity.Property(e => e.PersonToPhoneNumberId).HasColumnName("PersonToPhoneNumberID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsPrimary)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonToPhoneNumber)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToPhoneNumber__PersonID");

                entity.HasOne(d => d.PhoneNumber)
                    .WithMany(p => p.PersonToPhoneNumber)
                    .HasForeignKey(d => d.PhoneNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonToPhoneNumber__PhoneNumberID");
            });

            modelBuilder.Entity<PersonType>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX__PersonType__Code");

                entity.HasIndex(e => e.Name)
                    .HasName("IX__PersonType__Name");

                entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasIndex(e => e.PhoneNumber1)
                    .HasName("IX__PhoneNumber__PhoneNumber");

                entity.HasIndex(e => e.PhoneNumberGlobalId)
                    .HasName("IX__PhoneNumber__PhoneNumberGlobalID");

                entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsOnDnclist).HasColumnName("IsOnDNCList");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber1)
                    .IsRequired()
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberGlobalId)
                    .IsRequired()
                    .HasColumnName("PhoneNumberGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.PhoneNumberNumeric)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PhoneNumberTypeID");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.PhoneNumberType)
                    .WithMany(p => p.PhoneNumber)
                    .HasForeignKey(d => d.PhoneNumberTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhoneNumber__PhoneNumberTypeID");
            });

            modelBuilder.Entity<PhoneNumberHistory>(entity =>
            {
                entity.Property(e => e.PhoneNumberHistoryId).HasColumnName("PhoneNumberHistoryID");

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HistoryCreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryCreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsOnDnclist).HasColumnName("IsOnDNCList");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberGlobalId)
                    .IsRequired()
                    .HasColumnName("PhoneNumberGlobalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");

                entity.Property(e => e.PhoneNumberNumeric)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PhoneNumberTypeID");
            });

            modelBuilder.Entity<PhoneNumberType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX__PhoneNumberType__Name");

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PhoneNumberTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
