using MediatR;

namespace Application.Mediatr.Commands
{
    public class DeletePrescriptionScheduleCommand : IRequest<object>
    {
        public Guid PrescriptionScheduleKey { get; set; }

        public class Handler : IRequestHandler<DeletePrescriptionScheduleCommand, object>
        {
            public Handler()
            {
                // Inject dependencies here
            }

            public async Task<object> Handle(DeletePrescriptionScheduleCommand request, CancellationToken cancellationToken)
            {
                // TODO: Implement delete logic
                return await Task.FromResult(new { Success = true });
            }
        }
    }
}