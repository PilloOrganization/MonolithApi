using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class CreateAccountCommand : IRequest<Guid>, IUserScopedRequest
    {
        public Guid AccountKey { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public long UserId { get; set; }

        public class Handler : IRequestHandler<CreateAccountCommand, Guid>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var account = new Account
                {
                    UserId = request.UserId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                };
                _unitOfWork.AccountRepository.Create(account);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return account.EntityKey;
            }
        }
    }
}
