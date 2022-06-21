namespace money_manager_api.Entities
{
    public class Permission : BaseEntity
    {
        public int Id { get; set; } // 1 - admin, 2 - user
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
