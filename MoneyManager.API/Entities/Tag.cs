namespace money_manager_api.Entities
{
    public class Tag : BaseEntity
    {
        public Guid Id { get; set; }

        public string? Emoji { get; set; }

        public string? Name { get; set; }
    }
}
