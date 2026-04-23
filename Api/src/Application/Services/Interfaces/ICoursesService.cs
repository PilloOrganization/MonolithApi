using Application.DataTransferObjects;

namespace Application.Services.Interfaces
{
    public interface ICoursesService
    {
        Task<IEnumerable<CourseDto>> GetByAccountKey(Guid accountKey);
        Task<IEnumerable<CourseDto>> GetByAccountId(long accountId);
    }
}
