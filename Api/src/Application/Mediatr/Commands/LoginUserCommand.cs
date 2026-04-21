using Application.DataTransferObjects;
using Application.UnitOfWorks.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Services.Interfaces;
using MediatR;

namespace Application.Mediatr.Commands
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public string UsernameOrPhoneOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public class Handler : IRequestHandler<LoginUserCommand, UserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
            {
                _unitOfWork = unitOfWork;
                _passwordHasher = passwordHasher;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                // TODO: understand in a runtime if the input is a username, phone number, or email and then query the database accordingly
                User? user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.UsernameOrPhoneOrEmail)
                    ?? await _unitOfWork.UserRepository.GetUserByPhoneAsync(request.UsernameOrPhoneOrEmail)
                    ?? await _unitOfWork.UserRepository.GetUserByEmailAsync(request.UsernameOrPhoneOrEmail);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                if (!_passwordHasher.Verify(request.Password, user.Password))
                {
                    throw new Exception("Invalid password");
                }
                var userDto = _mapper.Map<UserDto>(user);
                Account defaultAccount = (await _unitOfWork.AccountRepository.GetByUserIdAsync(user.Id)).Single(a => a.IsDefault);
                userDto.DefaultAccountDto = _mapper.Map<AccountDto>(defaultAccount);
                IEnumerable<Course> courses = await _unitOfWork.CourseRepository.GetByAccountIdAsync(defaultAccount.Id);
                userDto.DefaultAccountDto.Courses = _mapper.Map<IEnumerable<CourseDto>>(courses);
                if (courses.Any())
                {
                    var firstCourse = courses.First();
                    IEnumerable<PrescriptionSchedule> prescriptionSchedules = await _unitOfWork.PrescriptionScheduleRepository.GetByCourseIdAsync(firstCourse.Id);
                    userDto.DefaultAccountDto.Courses.First().PrescriptionSchedules = _mapper.Map<IEnumerable<PrescriptionScheduleDto>>(prescriptionSchedules);
                }
                return userDto;
            }
        }
    }
}