using Domain.Enums;

namespace Application.Persons.Queries.DTOs
{
    public class RelatedPersonDto
    {
        public int PersonId { get; set; }

        public RelationType RelationType { get; set; }
    }
}
