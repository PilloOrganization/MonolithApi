namespace Api.Models.Requests
{
    public class CreatePrescriptionScheduleRequest
    {
        public Guid CourseKey { get; set; }
        public string? MedicineName { get; set; }
        public Guid? MedicineKey { get; set; }
        public uint DurationInDays { get; set; }
        public DateOnly StartDate { get; set; }
        public List<TimeOnly> DailyDoseTimes { get; set; } = new List<TimeOnly>();
    }
}