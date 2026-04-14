using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class CreatePrescriptionScheduleCommand : IRequest<object>
    {
        public Guid AccountKey { get; set; }
        public string MedicineKey { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public uint? DurationInDays { get; set; }
        public List<DateTime> DailyDoseTimes { get; set; } = new List<DateTime>();
        public bool IsInfinite()
        {
            return !DurationInDays.HasValue;
        }

        public class Handler : IRequestHandler<CreatePrescriptionScheduleCommand, object>
        {
            public Handler()
            {
            }

            public async Task<object> Handle(CreatePrescriptionScheduleCommand request, CancellationToken cancellationToken)
            {
                var medicine = new Medicine
                {
                    Name = request.MedicineName
                };

                var prescriptionSchedule = new PrescriptionSchedule
                {
                    Medicine = medicine,
                    Doses = new List<Dose>()
                };

                if (!request.IsInfinite())
                {
                    for (uint day = 0; day < request.DurationInDays!.Value; day++)
                    {
                        foreach (var time in request.DailyDoseTimes)
                        {
                            var doseTime = request.StartDate.Date.AddDays(day).Add(time.TimeOfDay);
                            prescriptionSchedule.Doses.Add(new Dose
                            {
                                Time = doseTime,
                                IsTaken = false
                            });
                        }
                    }
                }
                else
                {
                    // TODO: Handle infinite schedules appropriately (e.g., by creating doses on demand or using a different approach)
                    // For infinite schedules, we can create doses for a reasonable future period (e.g., next 30 days)
                    //for (uint day = 0; day < 30; day++)
                    //{
                    //    foreach (var time in request.DailyDoseTimes)
                    //    {
                    //        var doseTime = request.StartDate.Date.AddDays(day).Add(time.TimeOfDay);
                    //        prescriptionSchedule.Doses.Add(new Dose
                    //        {
                    //            Time = doseTime,
                    //            IsTaken = false
                    //        });
                    //    }
                    //}
                }
                return await Task.FromResult(new { Success = true, Schedule = prescriptionSchedule });
            }
        }
    }
}