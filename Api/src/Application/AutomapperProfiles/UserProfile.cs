using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
