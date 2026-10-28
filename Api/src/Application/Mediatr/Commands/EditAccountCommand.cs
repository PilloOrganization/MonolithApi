using Application.UnitOfWorks.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class EditAccountCommand : IRequest<Unit>
    {
        public Guid AccountKey { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public class Handler : IRequestHandler<EditAccountCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(EditAccountCommand request, CancellationToken cancellationToken)
            {
                Account account = await _unitOfWork.AccountRepository.GetAsync(request.AccountKey);
                account.FirstName = request.FirstName;
                account.LastName = request.LastName;

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
