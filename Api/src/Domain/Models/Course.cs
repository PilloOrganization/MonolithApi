namespace Domain.Models
{
    public class Course : Entity
    {
        public string Name { get; set; } = string.Empty;
        public long AccountId { get; set; }
        public List<PrescriptionSchedule> PrescriptionSchedules { get; set; } = new List<PrescriptionSchedule>();
    }
}
