namespace Domain.Models
{
    public class User : Entity
    {
        public virtual List<Account> Accounts { get; set; } = new();
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!;
    }
}
