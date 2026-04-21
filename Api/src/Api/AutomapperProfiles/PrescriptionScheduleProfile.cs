using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;

namespace Api.AutomapperProfiles
{
    public class PrescriptionScheduleProfile : Profile
    {
        public PrescriptionScheduleProfile()
        {
            CreateMap<CreatePrescriptionScheduleRequest, CreatePrescriptionScheduleCommand>();
            //CreateMap<UpdatePrescriptionScheduleRequest, UpdatePrescriptionScheduleCommand>();
        }
    }
}