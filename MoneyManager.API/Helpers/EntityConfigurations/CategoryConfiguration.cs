using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
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
        }
    }
}
