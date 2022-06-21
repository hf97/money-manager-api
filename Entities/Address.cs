namespace money_manager_api.Entities
{
    public class Address : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
