using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        public async Task<ActionResult<object>> Register(RegisterUserRequest userRegisterRequest)
        {
            var command = _mapper.Map<RegisterUserCommand>(userRegisterRequest);
            var result = await _mediator.Send(command);
            return Created();
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<object>> Login(LoginUserRequest userLoginRequest)
        {
            var command = _mapper.Map<LoginUserCommand>(userLoginRequest);
            var result = await _mediator.Send(command);
            return Ok(result);
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
