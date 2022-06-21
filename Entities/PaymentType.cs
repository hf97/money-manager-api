namespace money_manager_api.Entities
{
    public class PaymentType : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
