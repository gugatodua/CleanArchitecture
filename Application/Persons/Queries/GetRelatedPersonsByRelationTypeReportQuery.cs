using Domain.Enums;
using MediatR;

namespace Application.Persons.Queries
{
    public class GetRelatedPersonsByRelationTypeReportQuery : IRequest<IEnumerable<RelatedPersonReportDto>>
    {
    }

    public class GetRelatedPersonsByRelationTypeReportQueryHandler : IRequestHandler<GetRelatedPersonsByRelationTypeReportQuery, IEnumerable<RelatedPersonReportDto>>
    {
        private readonly IPersonRepository _personRepository;

        public GetRelatedPersonsByRelationTypeReportQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<RelatedPersonReportDto>> Handle(GetRelatedPersonsByRelationTypeReportQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetRelatedPersonReportByRelationTypeReportAsync();
        }
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
