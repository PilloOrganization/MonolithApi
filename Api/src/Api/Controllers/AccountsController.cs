using Api.Models.Requests;
using Application.DataTransferObjects;
using Application.Mediatr.Commands;
using Application.Mediatr.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("user/{userKey}")]
        public async Task<ActionResult<object>> GetAllForAccount(Guid userKey)
        {
            IEnumerable<AccountDto> result = await _mediator.Send(new GetAccountsQuery { UserKey = userKey });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> Post(CreateAccountRequest request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);
            Guid result = await _mediator.Send(command);
            return Ok(new { AccountKey = result });
        }

        [HttpDelete("{AccountKey}")]
        public async Task<ActionResult<object>> Delete(Guid accountKey)
        {
            var command = new DeleteAccountCommand { AccountKey = accountKey };
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
