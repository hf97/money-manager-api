namespace money_manager_api.Entities
{
    public class Account : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string? Icon { get; set; }

        public string Name { get; set; }

        public string? Iban { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        public bool UseInStatistics { get; set; } = true;

        public bool Archived { get; set; }
    }
}
