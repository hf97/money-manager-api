namespace money_manager_api.Entities
{
    public class SubCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Icon { get; set; }

        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
