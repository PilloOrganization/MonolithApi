using Api.Models.Requests;
using Application.DataTransferObjects;
using Application.Mediatr.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<object>> Register(RegisterUserRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);
            var result = await _mediator.Send(command);
            return Created();
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<object>> Login(LoginUserRequest request)
        {
            var command = _mapper.Map<LoginUserCommand>(request);
            UserDto userDto = await _mediator.Send(command);
            var token = new AuthentificationDetails.JwtProvider(HttpContext.RequestServices.GetRequiredService<IConfiguration>()).GenerateToken(userDto.EntityKey, userDto.Username);
            return Ok(new { Token = token, DefaultAccount = userDto.DefaultAccountDto });
        }

        //[HttpPost(nameof(Link))]
        //public ActionResult<object> Link()
        //{
        //    // TOD: link to fb, login with fb, etc.
        //    throw new NotImplementedException();
        //}

        //[HttpPost(nameof(LoginWithFacebook))]
        //public async Task<ActionResult<object>> LoginWithFacebook()
        //{
        //    // TOD: link to fb, login with fb, etc.
        //    throw new NotImplementedException();
        //}

        //[HttpPost(nameof(LoginWithSmsOTP))]
        //public async Task<ActionResult<object>> LoginWithSmsOTP()
        //{
        //    // TOD: link to fb, login with fb, etc.
        //    throw new NotImplementedException();
        //}
    }
}
