using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<Entities.User> builder)
        {
            builder.HasOne(u => u.Permission)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
