//using MediatR;
//using System;

//namespace Application.Mediatr.Commands
//{
//    public class UpdatePrescriptionScheduleCommand : IRequest<object>
//    {
//        public Guid PrescriptionScheduleKey { get; set; }
//        public string Name { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;

//        public class Handler : IRequestHandler<UpdatePrescriptionScheduleCommand, object>
//        {
//            public Handler()
//            {
//                // Inject dependencies here
//            }

//            public async Task<object> Handle(UpdatePrescriptionScheduleCommand request, CancellationToken cancellationToken)
//            {
//                // TODO: Implement update logic
//                return await Task.FromResult(new { Success = true });
//            }
//        }
//    }
//}