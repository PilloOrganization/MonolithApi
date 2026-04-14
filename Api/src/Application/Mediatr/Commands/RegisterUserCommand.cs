using Application.UnitOfWorks.Interfaces;
using Domain.Models;
using Domain.Services.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class RegisterUserCommand : IRequest<object>
    {
        public string Username { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public class Handler : IRequestHandler<RegisterUserCommand, object>
        {
            private readonly IUnitOfWork _unitOfWork;

            private readonly IPasswordHasher _passwordHasher;

            public Handler(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
            {
                _passwordHasher = passwordHasher;
                _unitOfWork = unitOfWork;
            }

            public async Task<object> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var hashedPassword = _passwordHasher.Hash(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Password = hashedPassword
                };

                await _unitOfWork.userRepository.CreateAsync(user);
                var account = new Account
                {
                    UserId = user.Id
                };
                await _unitOfWork.accountRepository.CreateAsync(account);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new { Success = true, User = user.Username });
            }
        }
    }
}