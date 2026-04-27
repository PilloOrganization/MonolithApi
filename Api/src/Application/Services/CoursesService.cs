using Application.DataTransferObjects;
using Application.Services.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;

namespace Application.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CoursesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetByAccountKey(Guid accountKey)
        {
            List<Course> courses = await _unitOfWork.CourseRepository.GetByAccountKeyAsync(accountKey);
            return await LoadFirstActiveCourseAndMapRest(courses);
        }

        public async Task<IEnumerable<CourseDto>> GetByAccountId(long accountId)
        {
            List<Course> courses = await _unitOfWork.CourseRepository.GetByAccountIdAsync(accountId);
            return await LoadFirstActiveCourseAndMapRest(courses);
        }

        private async Task<IEnumerable<CourseDto>> LoadFirstActiveCourseAndMapRest(List<Course> courses)
        {
            if (courses.Any(e => e.IsActive))
            {
                courses[0] = await _unitOfWork.CourseRepository.GetAsync(courses[0].Id);
            }

            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
    }
}
