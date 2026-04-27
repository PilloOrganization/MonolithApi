namespace Application.DataTransferObjects
{
    public class AccountDto : BaseDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
