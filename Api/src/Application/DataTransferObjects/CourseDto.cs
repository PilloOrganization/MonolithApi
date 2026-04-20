namespace Application.DataTransferObjects
{
    public class CourseDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<PrescriptionScheduleDto> PrescriptionSchedules { get; set; } = new List<PrescriptionScheduleDto>();
    }
}
