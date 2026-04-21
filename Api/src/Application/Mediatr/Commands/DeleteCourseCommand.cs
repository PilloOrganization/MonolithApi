using Application.UnitOfWorks.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class DeleteCourseCommand : IRequest<object>
    {
        public Guid CourseKey { get; set; }

        public class Handler : IRequestHandler<DeleteCourseCommand, object>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<object> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            {
                var course = await _unitOfWork.CourseRepository.GetAsync(request.CourseKey);
                _unitOfWork.CourseRepository.Delete(course);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(new { Success = true });
            }
        }
    }
}