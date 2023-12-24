using Application.Persons.Queries.DTOs;
using MediatR;

namespace Application.Persons.Queries
{
    public class PersonQuickSearchQuery : IRequest<IEnumerable<PersonDto>>
    {
        public string Keyword { get; }

        public PersonQuickSearchQuery(string keyword)
        {
            Keyword = keyword;
        }
    }

    public class PersonQuickSearchQueryHandler : IRequestHandler<PersonQuickSearchQuery, IEnumerable<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public PersonQuickSearchQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>> Handle(PersonQuickSearchQuery request, CancellationToken cancellationToken)
        {
           return await _personRepository.QuickSearchPerson(request.Keyword);
        }
    }
}
