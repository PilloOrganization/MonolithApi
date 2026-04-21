using Application.DataTransferObjects;
using AutoMapper;
using Domain.Models;

namespace Application.AutomapperProfiles
{
    public class PrescriptionScheduleProfile : Profile
    {
        public PrescriptionScheduleProfile()
        {
            CreateMap<PrescriptionSchedule, PrescriptionScheduleDto>();
        }
    }
}
