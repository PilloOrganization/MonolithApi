using Application.DataTransferObjects;
using Application.Mediatr.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetMedicinesForUserQuery : IRequest<IEnumerable<MedicineDto>>, IUserScopedRequest
    {
        public long UserId { get; set; }

        public class Handler : IRequestHandler<GetMedicinesForUserQuery, IEnumerable<MedicineDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MedicineDto>> Handle(GetMedicinesForUserQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Medicine> medicines = await _unitOfWork.MedicineRepository.GetByUserIdAsync(request.UserId);
                return _mapper.Map<IEnumerable<MedicineDto>>(medicines);
            }
        }
    }
}
