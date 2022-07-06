namespace money_manager_api.Entities
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Icon { get; set; }

        public Guid ColorId { get; set; }
        public Color Color { get; set; }
    }
}
