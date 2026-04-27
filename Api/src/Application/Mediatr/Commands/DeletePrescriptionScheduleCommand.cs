using Application.UnitOfWorks.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class DeletePrescriptionScheduleCommand : IRequest<Unit>
    {
        public Guid PrescriptionScheduleKey { get; set; }

        public class Handler : IRequestHandler<DeletePrescriptionScheduleCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeletePrescriptionScheduleCommand request, CancellationToken cancellationToken)
            {
                PrescriptionSchedule prescriptionSchedule = await _unitOfWork.PrescriptionScheduleRepository.GetAsync(request.PrescriptionScheduleKey);
                _unitOfWork.PrescriptionScheduleRepository.Delete(prescriptionSchedule);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}