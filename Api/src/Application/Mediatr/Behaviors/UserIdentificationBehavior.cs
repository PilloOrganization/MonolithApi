using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using Domain.Services.Interfaces;
using MediatR;

namespace Application.Mediatr.Behaviors
{
    public class UserIdentificationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        public UserIdentificationBehavior(IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (request is IUserScopedRequest scopedRequest)
            {
                var userKey = _userContext.UserKey;
                scopedRequest.UserId = await _unitOfWork.UserRepository.GetIdByKeyAsync(userKey);
            }
            return await next();
        }
    }
}
