namespace money_manager_api.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
    }
}
