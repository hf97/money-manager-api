namespace money_manager_api.Entities
{
    public class Color : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string HexadecimalValue { get; set; }
    }
}
