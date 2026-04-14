namespace Api.Models.Requests
{
    public class CreateCourseRequest
    {
        public Guid AccountKey { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}