using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class CreateCourseCommand : IRequest<Guid>
    {
        public Guid AccountKey { get; set; }
        public string Name { get; set; } = string.Empty;

        public class Handler : IRequestHandler<CreateCourseCommand, Guid>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
            {
                Account account = await _unitOfWork.AccountRepository.GetAsync(request.AccountKey);
                var course = new Course
                {
                    Name = request.Name,
                    AccountId = account.Id
                };
                _unitOfWork.CourseRepository.Create(course);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return course.EntityKey;
            }
        }
    }
}