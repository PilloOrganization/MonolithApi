namespace Application.DataTransferObjects
{
    public class CourseDto : BaseDto
    {
        public string Name { get; set; } = null!;
        public IEnumerable<PrescriptionScheduleDto> PrescriptionSchedules { get; set; } = new List<PrescriptionScheduleDto>();
    }
}
