using Domain.Models;
using MediatR;

namespace Application.Mediatr.Queries
{
    public class GetCoursesQuery : IRequest<IEnumerable<Course>>
    {
        public class Handler : IRequestHandler<GetCoursesQuery, IEnumerable<Course>>
        {
            // Inject your data source (e.g., DbContext, repository) here

            public Handler()
            {
                // Assign dependencies here
            }

            public async Task<IEnumerable<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                // TODO: Replace with actual data access logic
                return await Task.FromResult(new List<Course>());
            }
        }
    }
}