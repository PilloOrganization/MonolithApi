namespace Domain.Models
{
    public class PrescriptionSchedule : Entity
    {
        public long CourseId { get; set; }
        public long MedicineId { get; set; }
        public Medicine Medicine { get; set; } = new Medicine();
        public List<Dose> Doses { get; set; } = new List<Dose>();
    }
}
