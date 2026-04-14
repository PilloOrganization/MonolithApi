using MediatR;

namespace Application.Mediatr.Commands
{
    public class CreateCourseCommand : IRequest<object>
    {
        public Guid AccountKey { get; set; }
        public string Name { get; set; } = string.Empty;

        public class Handler : IRequestHandler<CreateCourseCommand, object>
        {
            public Handler()
            {
                // Assign dependencies here
            }

            public async Task<object> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
            {
                // TODO: Implement creation logic
                return await Task.FromResult(new { Success = true });
            }
        }
    }
}