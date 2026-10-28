using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;

namespace Api.AutomapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountRequest, CreateAccountCommand>();
            CreateMap<EditAccountRequest, EditAccountCommand>();
        }
    }
}
