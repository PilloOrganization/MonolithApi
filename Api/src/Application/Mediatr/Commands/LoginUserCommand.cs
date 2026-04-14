using Application.UnitOfWorks.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class LoginUserCommand : IRequest<object>
    {
        public string UsernameOrPhoneOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public class Handler : IRequestHandler<LoginUserCommand, object>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<object> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                // TODO: Implement actual login logic (validate user, check password, etc.)
                // Example response:
                return await Task.FromResult(new { Success = true, User = request.UsernameOrPhoneOrEmail });
            }
        }
    }
}