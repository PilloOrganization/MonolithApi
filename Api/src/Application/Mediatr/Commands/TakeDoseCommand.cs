using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class TakeDoseCommand : IRequest<Unit>, IUserScopedRequest
    {
        public long UserId { get; set; }
        public Guid DoseKey { get; set; }
        public bool TakeUntake { get; set; }

        public class Handler : IRequestHandler<TakeDoseCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(TakeDoseCommand request, CancellationToken cancellationToken)
            {
                await _unitOfWork.DoseRepository.UpdateIsTakenAsync(request.DoseKey, request.TakeUntake);
                return Unit.Value;
            }
        }
    }
}
