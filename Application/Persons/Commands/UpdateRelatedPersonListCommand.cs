using Application.Persons.Queries.DTOs;
using MediatR;

namespace Application.Persons.Commands
{
    public class UpdateRelatedPersonListCommand : IRequest<Unit>
    {
        public int PersonId { get; set; }
        public List<RelatedPersonDto> RelatedPersons { get; set; }
    }
}
