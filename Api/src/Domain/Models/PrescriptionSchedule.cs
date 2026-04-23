namespace Domain.Models
{
    public class PrescriptionSchedule : Entity
    {
        public long CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public long MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual List<Dose> Doses { get; set; } = new();
        public override void Delete()
        {
            base.Delete();
            foreach (Dose dose in Doses)
            {
                dose.Delete();
            }
        }
    }
}
