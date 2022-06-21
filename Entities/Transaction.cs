using Microsoft.EntityFrameworkCore;

namespace money_manager_api.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        [Precision(18,2)]
        public decimal Quantity { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }

        public Guid? TagId { get; set; }
        public Tag? Tag { get; set; }

        public string? Beneficiary { get; set; }

        public int PaymentTypeId { get; set; } = 1;
        public PaymentType PaymentType { get; set; }

        public DateTime? Warranty { get; set; }

        public int StatusId { get; set; } = 1;
        public Status Status { get; set; }

        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }

        public bool IsRecurrent { get; set; }

        public bool UseInStatistics { get; set; } = true;

        public bool Archived { get; set; }
    }
}
