namespace Domain.Models
{
    public class PrescriptionSchedule : Entity
    {
        public long CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        public long MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; } = new Medicine();
        public virtual List<Dose> Doses { get; set; } = new List<Dose>();
    }
}
