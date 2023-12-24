using Application.Persons.Queries.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Persons.Queries
{
    public class PersonDetailedSearchQuery : IRequest<IEnumerable<PersonDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string PersonalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PersonDetailedSearchQueryHandler : IRequestHandler<PersonDetailedSearchQuery, IEnumerable<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;

        public PersonDetailedSearchQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>> Handle(PersonDetailedSearchQuery personDetailedSearchQuery, CancellationToken cancellationToken)
        {
            return await _personRepository.DetailSearchPerson(personDetailedSearchQuery);
        }
    }
}
