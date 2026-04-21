using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
