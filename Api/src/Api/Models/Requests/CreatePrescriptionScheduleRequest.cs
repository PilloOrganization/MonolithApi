namespace Api.Models.Requests
{
    public class CreatePrescriptionScheduleRequest
    {
        public Guid AccountKey { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public string MedicineKey { get; set; } = string.Empty;
        public uint DurationInDays { get; set; }
        public DateOnly StartDate { get; set; }
        public List<TimeOnly> DailyDoseTimes { get; set; } = new List<TimeOnly>();
    }
}