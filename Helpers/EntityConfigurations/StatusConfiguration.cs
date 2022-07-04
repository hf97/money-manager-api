using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
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
        }
    }
}
