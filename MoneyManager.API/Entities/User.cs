namespace money_manager_api.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime JoiningAt { get; set; } = DateTime.UtcNow;

        public int PermissionId { get; set; } = 2;
        public Permission Permission { get; set; }

        //public byte[] PasswordHash { get; set; }

        //public byte[] PasswordSalt { get; set; }

        //public Guid RecoverToken { get; set; } = Guid.Empty;

        //public DateTime? RecoverExpiration { get; set; }
    }
}
