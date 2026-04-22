namespace Domain.Models
{
    public class Dose : Entity
    {
        public long PrescriptionScheduleId { get; set; }
        public virtual PrescriptionSchedule PrescriptionSchedule { get; set; } = null!;
        public DateTime Time { get; set; }
        public bool IsTaken { get; set; }
    }
}
