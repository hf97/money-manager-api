namespace money_manager_api.Entities
{
    public class TransactionAttachment : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public Guid AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
