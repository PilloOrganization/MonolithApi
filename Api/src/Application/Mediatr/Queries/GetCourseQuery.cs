using Application.DataTransferObjects;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetCourseQuery : IRequest<CourseDto>
    {
        public Guid CourseKey { get; set; }

        public class Handler : IRequestHandler<GetCourseQuery, CourseDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
            {
                Course course = await _unitOfWork.CourseRepository.GetAsync(request.CourseKey);
                var courseDto = _mapper.Map<CourseDto>(course);
                return courseDto;
            }
        }
    }
}