using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using money_manager_api.Entities;

namespace money_manager_api.Helpers.EntityConfigurations
{
    public class TransactionAttachmentConfiguration : IEntityTypeConfiguration<TransactionAttachment>
    {
        public void Configure(EntityTypeBuilder<TransactionAttachment> builder)
        {
            builder.HasOne(ta => ta.Transaction)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ta => ta.Attachment)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
