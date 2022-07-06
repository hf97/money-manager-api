namespace money_manager_api.Entities
{
    public class Attachment : BaseEntity
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }
    }
}
