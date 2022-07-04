using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Currency)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
