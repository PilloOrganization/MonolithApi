using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;

namespace Api.AutomapperProfiles
{
    public class DoseProfile : Profile
    {
        public DoseProfile()
        {
            CreateMap<TakeDoseRequest, TakeDoseCommand>();
            //CreateMap<UpdatePrescriptionScheduleRequest, UpdatePrescriptionScheduleCommand>();
        }
    }
}
