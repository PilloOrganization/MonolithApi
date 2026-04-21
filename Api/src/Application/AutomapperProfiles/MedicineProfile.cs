using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<Medicine, MedicineDto>();
        }
    }
}
