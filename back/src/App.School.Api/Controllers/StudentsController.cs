using App.School.Application.Students.Commands;
using App.School.Application.Students.Queries;
using App.School.Application.Students.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.School.Api4.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method responsible for fetch all students
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet()]
        public async Task<ActionResult<StudentViewModel>> GetStudents([FromQuery(Name = "name")] string? name)
        {
            var response = await _mediator.Send(new GetStudentsQuery(name));
            return await Task.FromResult(Ok(response.Value));
        }

        /// <summary>
        /// Method responsible for registering a student
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentViewModel>> Post([FromBody] CreateStudentCommand command)
        {
            if (command == null)
                return new BadRequestObjectResult("Invalid request");

            var response = await _mediator.Send(command);
            if (response.IsError)
            {
                ModelState.AddModelError("Invalid_data", response.Error);
                return ValidationProblem();
            }

            return await Task.FromResult(Ok(response.Value));
        }

        /// <summary>
        /// Method responsible for update a student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentViewModel>> Patch(int id, [FromBody] UpdateStudentCommand command)
        {
            if (command == null)
                return new BadRequestObjectResult("Invalid request");

            var response = await _mediator.Send(command.SetId(id));
            if (response.IsError)
            {
                ModelState.AddModelError("Invalid_data", response.Error);
                return ValidationProblem();
            }

            return await Task.FromResult(Ok(response.Value));
        }

        /// <summary>
        /// Method responsible for update a student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _mediator.Send(new DeleteStudentCommand(id));
            if (response.IsError)
            {
                ModelState.AddModelError("Invalid_data", response.Error);
                return ValidationProblem();
            }

            return await Task.FromResult(Ok());
        }
    }
}