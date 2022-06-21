namespace money_manager_api.Entities
{
    public class Currency : BaseEntity
    {
        public Guid Id { get; set; }

        public string Iso { get; set; }

        public string Name { get; set; }
    }
}
