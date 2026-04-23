namespace Application.DataTransferObjects
{
    public class PrescriptionScheduleDto : BaseDto
    {
        public MedicineDto Medicine { get; set; } = null!;
        public IEnumerable<DoseDto> Doses { get; set; } = new List<DoseDto>();
    }
}
