namespace Api.Models.Requests
{
    public class CreatePrescriptionScheduleRequest
    {
        public Guid AccountKey { get; set; }
        public string MedicineName { get; set; } = string.Empty;
        public string MedicineKey { get; set; } = string.Empty;
        public uint? DurationInDays { get; set; }
        public DateTime StartDate { get; set; }
        public List<DateTime> DailyDoseTimes { get; set; } = new List<DateTime>();
    }
}