using Api.Models.Requests;
using Application.DataTransferObjects;
using Application.Mediatr.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DosesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut(nameof(Take))]
        public async Task<ActionResult<object>> Take(TakeDoseRequest request)
        {
            var command = _mapper.Map<TakeDoseCommand>(request);
            var result = await _mediator.Send(command);
            return Ok();
        }
    }
}
