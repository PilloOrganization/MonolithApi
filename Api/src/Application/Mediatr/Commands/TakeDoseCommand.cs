using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(TakeDoseCommand request, CancellationToken cancellationToken)
            {
                await _unitOfWork.DoseRepository.UpdateIsTakenAsync(request.DoseKey, request.TakeUntake);
                return Unit.Value;
            }
        }
    }
}
