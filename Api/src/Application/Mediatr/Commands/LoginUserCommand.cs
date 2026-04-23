using Application.DataTransferObjects;
using Application.Services.Interfaces;
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
            private readonly ICoursesService _coursesService;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, ICoursesService coursesService)
            {
                _unitOfWork = unitOfWork;
                _passwordHasher = passwordHasher;
                _mapper = mapper;
                _coursesService = coursesService;
            }

            public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                // TODO: understand in a runtime if the input is a username, phone number, or email and then query the database accordingly
                User? user = await _unitOfWork.UserRepository.GetByUsernameAsync(request.UsernameOrPhoneOrEmail)
                    ?? await _unitOfWork.UserRepository.GetByPhoneAsync(request.UsernameOrPhoneOrEmail)
                    ?? await _unitOfWork.UserRepository.GetByEmailAsync(request.UsernameOrPhoneOrEmail);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                if (!_passwordHasher.Verify(request.Password, user.Password))
                {
                    throw new Exception("Invalid password");
                }
                var userDto = _mapper.Map<UserDto>(user);
                Account defaultAccount = user.Accounts.Single(a => a.IsDefault);
                userDto.DefaultAccountDto = _mapper.Map<AccountDto>(defaultAccount);
                IEnumerable<CourseDto> courseDtos = await _coursesService.GetByAccountId(defaultAccount.Id);
                userDto.DefaultAccountDto.Courses = courseDtos;
                return userDto;
            }
        }
    }
}