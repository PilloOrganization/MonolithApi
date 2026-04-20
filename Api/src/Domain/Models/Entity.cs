namespace Domain.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            EntityKey = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
        }
        public long Id { get; set; }
        public Guid EntityKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
