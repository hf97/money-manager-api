namespace money_manager_api.Entities
{
    public abstract class BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
        public string? ModifiedBy { get; set; }
    }
}
