using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class DoseProfile : Profile
    {
        public DoseProfile()
        {
            CreateMap<Dose, DoseDto>();
        }
    }
}
