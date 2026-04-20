using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;

namespace Api.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUserCommand>();
            CreateMap<LoginUserRequest, LoginUserCommand>();
        }
    }
}