using Application.DataTransferObjects;
using Application.Mediatr.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> Get()
        {
            var result = await _mediator.Send(new GetMedicinesForUserQuery());
            return Ok(result);
        }
    }
}
