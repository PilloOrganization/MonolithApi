using Application.DataTransferObjects;
using Application.Services.Interfaces;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetCoursesQuery : IRequest<IEnumerable<CourseDto>>
    {
        public Guid AccountKey { get; init; }

        public class Handler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICoursesService _coursesService;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, ICoursesService coursesService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _coursesService = coursesService;
            }

            public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<CourseDto> courseDtos = await _coursesService.GetByAccountKey(request.AccountKey);
                return courseDtos;
            }
        }
    }
}