namespace Domain.Models
{
    public class Account : Entity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsDefault { get; set; }

    }
}
