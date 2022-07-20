namespace money_manager_api.Entities
{
    public class Country : BaseEntity
    {
        public Guid Id { get; set; }

        public string Iso { get; set; }

        public string Name { get; set; }

        public string Culture { get; set; }
    }
}
