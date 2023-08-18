namespace Api.Controllers
{
    using Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Task/Statistics
        /// <summary>
        /// Get Statistics
        /// </summary>
        [HttpGet]
        [Route("Statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _mediator.Send(new GetStatisticsQuery());
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // GET: api/Task
        /// <summary>
        /// Get Tasks
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTaskQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // POST: api/Task
        /// <summary>
        /// Add Task
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // PUT: api/Task
        /// <summary>
        /// Update Task
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }

        // DELETE: api/Task/5
        /// <summary>
        /// Delete Task
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }
    }
}
