namespace Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Guid EntityKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
