using Api.Models.Requests;
using Application.Mediatr.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PrescriptionScheduleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Post(CreatePrescriptionScheduleRequest request)
        {
            var command = _mapper.Map<CreatePrescriptionScheduleCommand>(request);
            var result = await _mediator.Send(command);
            return Created();
        }

        //[HttpPut("{PrescriptionScheduleKey}")]
        //public async Task<ActionResult<object>> Put(Guid PrescriptionScheduleKey, CreatePrescriptionScheduleRequest request)
        //{
        //    var command = _mapper.Map<UpdatePrescriptionScheduleCommand>(request);
        //    command.PrescriptionScheduleKey = PrescriptionScheduleKey;
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        [HttpDelete("{PrescriptionScheduleKey}")]
        public async Task<ActionResult<object>> Delete(Guid PrescriptionScheduleKey)
        {
            var command = new DeletePrescriptionScheduleCommand { PrescriptionScheduleKey = PrescriptionScheduleKey };
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
