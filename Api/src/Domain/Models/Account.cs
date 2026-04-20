namespace Domain.Models
{
    public class Account : Entity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; } = new User();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;

    }
}
