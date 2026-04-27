using Application.DataTransferObjects;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetAccountsQuery : IRequest<IEnumerable<AccountDto>>
    {
        public Guid UserKey { get; init; }

        public class Handler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Account> accounts = await _unitOfWork.AccountRepository.GetByUserKeyAsync(request.UserKey);
                var accountDtos = _mapper.Map<IEnumerable<AccountDto>>(accounts);
                return accountDtos;
            }
        }
    }
}
