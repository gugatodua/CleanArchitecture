using Application.Persons.Queries;
using MediatR;

namespace Application.Persons.QueryHandlers
{
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
}