using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;

namespace money_manager_api.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_configuration.GetConnectionString("MoneyManagerDatabase"));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionAttachment> TransactionAttachments{ get; set; }
        public DbSet<TransactionType> TransactionTypes{ get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Currency)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Category>()
            //    .HasData();

            //modelBuilder.Entity<Color>()
            //    .HasData();

            //modelBuilder.Entity<Country>()
            //    .HasData();

            //modelBuilder.Entity<Currency>()
            //    .HasData();

            //modelBuilder.Entity<PaymentType>()
            //    .HasData();

            modelBuilder.Entity<Permission>()
                .HasData(
                    new Permission
                    {
                        Id = 1,
                        Name = "admin",
                        Description = "admin"
                    },
                    new Permission
                    {
                        Id = 2,
                        Name = "user",
                        Description = "user"
                    }
                );

            modelBuilder.Entity<Status>()
                .HasData(
                    new Status
                    {
                        Id = 1,
                        Name = "Complete",
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatedBy = "admin",
                        ModifiedAt = DateTimeOffset.UtcNow,
                        ModifiedBy = "admin"
                    },
                    new Status
                    {
                        Id = 2,
                        Name = "Incomplete",
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatedBy = "admin",
                        ModifiedAt = DateTimeOffset.UtcNow,
                        ModifiedBy = "admin"
                    }
                );

            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Category)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SubCategory>()
            //    .HasData();

            modelBuilder.Entity<TransactionType>()
                .HasData(
                    new TransactionType
                    {
                        Id = 1,
                        Name = "Expense",
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatedBy = "admin",
                        ModifiedAt = DateTimeOffset.UtcNow,
                        ModifiedBy = "admin"
                    },
                    new TransactionType
                    {
                        Id = 2,
                        Name = "Income",
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatedBy = "admin",
                        ModifiedAt = DateTimeOffset.UtcNow,
                        ModifiedBy = "admin"
                    },
                    new TransactionType
                    {
                        Id = 3,
                        Name = "Transfer",
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatedBy = "admin",
                        ModifiedAt = DateTimeOffset.UtcNow,
                        ModifiedBy = "admin"
                    }
                );

            modelBuilder.Entity<TransactionAttachment>()
                .HasOne(ta => ta.Transaction)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionAttachment>()
                .HasOne(ta => ta.Attachment)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Permission)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            string username = _httpContextAccessor.HttpContext.User.Identity.Name;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedBy = username;
                    ((BaseEntity)entity.Entity).CreatedAt = DateTimeOffset.UtcNow;
                }

                ((BaseEntity)entity.Entity).ModifiedBy = username;
                ((BaseEntity)entity.Entity).ModifiedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            string username = _httpContextAccessor.HttpContext.User.Identity.Name;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedBy = username;
                    ((BaseEntity)entity.Entity).CreatedAt = DateTimeOffset.UtcNow;
                }

                ((BaseEntity)entity.Entity).ModifiedBy = username;
                ((BaseEntity)entity.Entity).ModifiedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
