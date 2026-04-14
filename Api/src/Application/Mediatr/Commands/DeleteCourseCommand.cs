using MediatR;

namespace Application.Mediatr.Commands
{
    public class DeleteCourseCommand : IRequest<object>
    {
        public Guid CourseKey { get; set; }

        public class Handler : IRequestHandler<DeleteCourseCommand, object>
        {
            public Handler()
            {
                // Assign dependencies here
            }

            public async Task<object> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            {
                // TODO: Implement delete logic
                return await Task.FromResult(new { Success = true });
            }
        }
    }
}