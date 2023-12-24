using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using Domain;

namespace Application.Persons
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        void Delete(Person person);
        Task<IEnumerable<Person>> GetAllAsync(Person person);
        Task<IEnumerable<RelatedPersonReportDto>> GetRelatedPersonReportByRelationTypeReportAsync();
        Task<Person> GetByIdAsync(int id);
        void Update(Person person);
        Task UpdateRelatedPersonsAsync(int personId, IEnumerable<RelatedPersonDto> relatedPersonDtos);
        Task<IEnumerable<PersonDto>> QuickSearchPerson(string keyword);
        Task<IEnumerable<PersonDto>> DetailSearchPerson(PersonDetailedSearchQuery detailedSearchQuery);
    }
}