using Application.Persons.Commands;
using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TBCInterviewProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ModelValidation]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PersonDto> GetPerson([FromBody] GetPersonQuery getPersonQuery)
        {
            var result = await _mediator.Send(getPersonQuery);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelatedPersonsByRelationTypeReport(GetRelatedPersonsByRelationTypeReportQuery getRelatedPersonsByRelationTypeReportQuery)
        {
            return Ok(await _mediator.Send(getRelatedPersonsByRelationTypeReportQuery));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand createPersonCommand)
        {
            await _mediator.Send(createPersonCommand);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand updatePersonCommand)
        {
            await _mediator.Send(updatePersonCommand);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRelatedPersonList([FromBody] UpdateRelatedPersonListCommand updateRelatedPersonListCommand)
        {
            await _mediator.Send(updateRelatedPersonListCommand);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson([FromBody] DeletePersonCommand deletePersonCommand)
        {
            await _mediator.Send(deletePersonCommand);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UploadPicture([FromBody] UploadPictureCommand uploadPictureCommand)
        {
            await _mediator.Send(uploadPictureCommand);

            return NoContent();
        }
    }
}