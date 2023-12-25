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
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
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

        [HttpGet("Related-persons-report")]
        public async Task<IActionResult> GetRelatedPersonsByRelationTypeReport()
        {
            return Ok(await _mediator.Send(new GetRelatedPersonsByRelationTypeReportQuery()));
        }

        [HttpGet("Quick-search")]
        public async Task<IActionResult> QuickSearchPerson(string? keyword)
        {
            var query = new PersonQuickSearchQuery(keyword);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> DetailedSearch(
            string? firstName,
            string? lastName,
            Gender? gender,
            string? personalId,
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand createPersonCommand)
        {
            await _mediator.Send(createPersonCommand);

            return NoContent();
        }

        [HttpPost("Add-related-person")]
        public async Task<IActionResult> AddRelatedPerson([FromBody] AddRelatedPersonCommand addRelatedPersonCommand)
        {
            await _mediator.Send(addRelatedPersonCommand);

            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand updatePersonCommand)
        {
            await _mediator.Send(updatePersonCommand);

            return NoContent();
        }

        [HttpPut("Update-related-people")]
        public async Task<IActionResult> UpdateRelatedPersonList([FromBody] UpdateRelatedPersonListCommand updateRelatedPersonListCommand)
        {
            await _mediator.Send(updateRelatedPersonListCommand);

            return NoContent();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePerson([FromBody] DeletePersonCommand deletePersonCommand)
        {
            await _mediator.Send(deletePersonCommand);

            return NoContent();
        }

        [HttpDelete("Delete-related-person")]
        public async Task<IActionResult> DeleteRelatedPerson([FromBody] DeleteRelatedPersonCommand deleteRelatedPersonCommand)
        {
            await _mediator.Send(deleteRelatedPersonCommand);

            return NoContent();
        }

        [HttpPut("Upload")]
        public async Task<IActionResult> UploadPicture(int personId, IFormFile file)
        {
            var uploadPictureCommand = new UploadPictureCommand
            {
                PersonId = personId,
                Picture = file
            };

            await _mediator.Send(uploadPictureCommand);

            return NoContent();
        }
    }
}