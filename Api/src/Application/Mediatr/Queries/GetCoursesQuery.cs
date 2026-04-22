using Application.DataTransferObjects;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
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

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                var courses = await _unitOfWork.CourseRepository.GetByAccountKeyAsync(request.AccountKey);
                if (courses.Any())
                {
                    var prescriptionSchedules = await _unitOfWork.PrescriptionScheduleRepository.GetByCourseKeyAsync(courses.First().EntityKey);
                }
                return _mapper.Map<IEnumerable<CourseDto>>(courses);
            }
        }
    }
}