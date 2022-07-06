using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasOne(s => s.Color)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Category)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SubCategory>()
            //    .HasData();
        }
    }
}
