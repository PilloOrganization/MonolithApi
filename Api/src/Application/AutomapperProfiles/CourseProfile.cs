using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
