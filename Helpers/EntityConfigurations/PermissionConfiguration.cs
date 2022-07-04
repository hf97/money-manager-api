using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
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
        }
    }
}
