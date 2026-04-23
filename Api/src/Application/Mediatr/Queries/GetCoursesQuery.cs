using Application.DataTransferObjects;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetCoursesQuery : IRequest<IEnumerable<CourseDto>>
    {
        public Guid AccountKey { get; init; }

        public class Handler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
        {
            private readonly ICoursesService _coursesService;

            public Handler(ICoursesService coursesService)
            {
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