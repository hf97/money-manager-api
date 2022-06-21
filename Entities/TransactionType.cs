namespace money_manager_api.Entities
{
    public class TransactionType : BaseEntity
    {
        public int Id { get; set; } // 1 - expense, 2 - income, 3 - tranfer

        public string Name { get; set; }
    }
}
