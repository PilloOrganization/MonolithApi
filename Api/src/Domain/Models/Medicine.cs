namespace Domain.Models
{
    public class Medicine : Entity
    {
        public long UserId { get; set; }
        public string Name { get; set; } = null!;
    }
}
