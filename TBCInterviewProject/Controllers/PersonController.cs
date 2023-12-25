using Application.Persons.Commands;
using Application.Persons.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TBCInterviewProject.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ModelValidation]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var getPersonQuery = new GetPersonQuery { Id = id };

            var result = await _mediator.Send(getPersonQuery);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("related-persons-report")]
        public async Task<IActionResult> GetRelatedPersonsByRelationTypeReport()
        {
            return Ok(await _mediator.Send(new GetRelatedPersonsByRelationTypeReportQuery()));
        }

        [HttpGet("quick-search")]
        public async Task<IActionResult> QuickSearchPerson(string keyword)
        {
            var query = new PersonQuickSearchQuery(keyword);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> DetailedSearch(
            string firstName,
            string lastName,
            Gender? gender,
            string personalId,
            DateTime? birthDate,
            int? cityId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = new PersonDetailedSearchQuery
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                PersonalId = personalId,
                BirthDate = birthDate,
                CityId = cityId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand createPersonCommand)
        {
            await _mediator.Send(createPersonCommand);

            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand updatePersonCommand)
        {
            await _mediator.Send(updatePersonCommand);

            return NoContent();
        }

        [HttpPut("update-related-people")]
        public async Task<IActionResult> UpdateRelatedPersonList([FromBody] UpdateRelatedPersonListCommand updateRelatedPersonListCommand)
        {
            await _mediator.Send(updateRelatedPersonListCommand);

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePerson([FromBody] DeletePersonCommand deletePersonCommand)
        {
            await _mediator.Send(deletePersonCommand);

            return NoContent();
        }

        [HttpPut("upload")]
        public async Task<IActionResult> UploadPicture([FromBody] UploadPictureCommand uploadPictureCommand)
        {
            await _mediator.Send(uploadPictureCommand);

            return NoContent();
        }
    }
}