using Application.UnitOfWorks.Interfaces;
using Domain.Models;
using Domain.Services.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public class Handler : IRequestHandler<RegisterUserCommand, Unit>
        {
            private readonly IUnitOfWork _unitOfWork;

            private readonly IPasswordHasher _passwordHasher;

            public Handler(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
            {
                _passwordHasher = passwordHasher;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                string effectiveUsername = GetEffectiveUsername(request);
                string hashedPassword = _passwordHasher.Hash(request.Password);
                var user = new User
                {
                    Username = effectiveUsername,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Password = hashedPassword,
                };
                user.Accounts.Add(new Account
                {
                    IsDefault = true
                });
                _unitOfWork.UserRepository.Create(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            private string GetEffectiveUsername(RegisterUserCommand request)
            {
                if (!string.IsNullOrWhiteSpace(request.Username))
                {
                    return request.Username;
                }
                if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    return request.PhoneNumber;
                }
                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    return request.Email;
                }

                return string.Empty;
            }
        }
    }
}