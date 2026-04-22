namespace Domain.Models
{
    public class Course : Entity
    {
        public string Name { get; set; } = string.Empty;
        public long AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;
        public List<PrescriptionSchedule> PrescriptionSchedules { get; set; } = new();
    }
}
