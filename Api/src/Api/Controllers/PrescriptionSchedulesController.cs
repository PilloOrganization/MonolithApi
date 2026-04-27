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
    public class PrescriptionSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PrescriptionSchedulesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionScheduleDto>> Post(CreatePrescriptionScheduleRequest request)
        {
            var command = _mapper.Map<CreatePrescriptionScheduleCommand>(request);
            PrescriptionScheduleDto result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{PrescriptionScheduleKey}")]
        public async Task<ActionResult<object>> Delete(Guid prescriptionScheduleKey)
        {
            var command = new DeletePrescriptionScheduleCommand { PrescriptionScheduleKey = prescriptionScheduleKey };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
