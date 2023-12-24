using Domain.Enums;
using MediatR;

namespace Application.Persons.Queries
{
    public class GetRelatedPersonsByRelationTypeReportQuery : IRequest<IEnumerable<RelatedPersonReportDto>>
    {
    }

    public class RelatedPersonReportDto
    {
        public int PersonId { get; set; }
        public List<RelationCount> RelationCounts { get; set; }
    }

    public class RelationCount
    {
        public RelationType RelationType { get; set; }
        public int Count { get; set; }
    }
}
