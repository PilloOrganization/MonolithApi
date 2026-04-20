namespace Application.DataTransferObjects
{
    public class AccountDto : BaseDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
