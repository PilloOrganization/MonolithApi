using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;

namespace Api.AutomapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            //CreateMap<UpdateCourseRequest, UpdateCourseCommand>();
        }
    }
}