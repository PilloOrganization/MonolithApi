using Application.UnitOfWorks.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class DeleteAccountCommand : IRequest<Unit>
    {
        public Guid AccountKey { get; set; }

        public class Handler : IRequestHandler<DeleteAccountCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
            {
                Account account = await _unitOfWork.AccountRepository.GetWithRelatedAsync(request.AccountKey);
                _unitOfWork.AccountRepository.Delete(account);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
