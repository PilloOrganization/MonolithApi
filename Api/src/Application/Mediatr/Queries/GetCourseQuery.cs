using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetCourseQuery : IRequest<Course>
    {
        public Guid CourseKey { get; set; }

        public class Handler : IRequestHandler<GetCourseQuery, Course>
        {
            // Inject your data source (e.g., DbContext, repository) here

            public Handler()
            {
                // Assign dependencies here
            }

            public async Task<Course> Handle(GetCourseQuery request, CancellationToken cancellationToken)
            {
                // TODO: Replace with actual data access logic
                return await Task.FromResult<Course>(null!);
            }
        }
    }
}