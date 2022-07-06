using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasData(
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
        }
    }
}
