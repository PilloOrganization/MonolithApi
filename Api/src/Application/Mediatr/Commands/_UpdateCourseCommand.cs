//using MediatR;
//using System;

//namespace Application.Mediatr.Commands
//{
//    public class UpdateCourseCommand : IRequest<object>
//    {
//        public Guid CourseKey { get; set; }
//        public string Name { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;

//        public class Handler : IRequestHandler<UpdateCourseCommand, object>
//        {
//            public Handler()
//            {
//                // Assign dependencies here
//            }

//            public async Task<object> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
//            {
//                // TODO: Implement update logic
//                return await Task.FromResult(new { Success = true });
//            }
//        }
//    }
//}