using Api.Models.Requests;
using Application.Mediatr.Commands;
using Application.Mediatr.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CourseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            var result = await _mediator.Send(new GetCoursesQuery());
            return Ok(result);
        }

        [HttpGet("{courseKey}")]
        public async Task<ActionResult<object>> Get(Guid courseKey)
        {
            var result = await _mediator.Send(new GetCourseQuery { CourseKey = courseKey });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> Post(CreateCourseRequest request)
        {
            var command = _mapper.Map<CreateCourseCommand>(request);
            var result = await _mediator.Send(command);
            return Created();
        }

        //[HttpPut("{courseKey}")]
        //public async Task<ActionResult<object>> Put(Guid courseKey, UpdateCourseRequest request)
        //{
        //    var command = _mapper.Map<UpdateCourseCommand>(request);
        //    command.CourseKey = courseKey;
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        [HttpDelete("{courseKey}")]
        public async Task<ActionResult<object>> Delete(Guid courseKey)
        {
            var command = new DeleteCourseCommand { CourseKey = courseKey };
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
