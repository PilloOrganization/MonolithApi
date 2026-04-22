using Application.DataTransferObjects;
using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class CreatePrescriptionScheduleCommand : IRequest<PrescriptionScheduleDto>, IUserScopedRequest
    {
        public Guid CourseKey { get; set; }
        public Guid? MedicineKey { get; set; }
        public string? MedicineName { get; set; }
        public DateOnly StartDate { get; set; }
        public uint DurationInDays { get; set; }
        public List<TimeOnly> DailyDoseTimes { get; set; } = new List<TimeOnly>();
        public long UserId { get; set; }

        public class Handler : IRequestHandler<CreatePrescriptionScheduleCommand, PrescriptionScheduleDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<PrescriptionScheduleDto> Handle(CreatePrescriptionScheduleCommand request, CancellationToken cancellationToken)
            {
                long courseId = await _unitOfWork.CourseRepository.GetIdAsync(request.CourseKey);
                Medicine medicine = await GetMedicine(request.MedicineKey, request.MedicineName!, request.UserId);
                var prescriptionSchedule = new PrescriptionSchedule
                {
                    CourseId = courseId,
                    Medicine = medicine
                };

                for (int day = 0; day < request.DurationInDays; day++)
                {
                    DateOnly currentDay = request.StartDate.AddDays(day);
                    foreach (var time in request.DailyDoseTimes)
                    {
                        DateTime exactDoseTime = currentDay.ToDateTime(time);
                        prescriptionSchedule.Doses.Add(new Dose
                        {
                            Time = exactDoseTime,
                            IsTaken = false
                        });
                    }
                }
                _unitOfWork.PrescriptionScheduleRepository.Create(prescriptionSchedule);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return _mapper.Map<PrescriptionScheduleDto>(prescriptionSchedule);
            }

            private async Task<Medicine> GetMedicine(Guid? medicineKey, string medicineName, long userId)
            {
                if (medicineKey.HasValue)
                {
                    return await _unitOfWork.MedicineRepository.GetAsync(medicineKey.Value);
                }
                return await _unitOfWork.MedicineRepository.GetAsync(medicineName, userId) ?? new Medicine { Name = medicineName, UserId = userId };
            }
        }
    }
}