using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using Domain;

namespace Application
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        void Delete(Person person);
        Task<IEnumerable<Person>> GetAllAsync(Person person);
        Task<IEnumerable<RelatedPersonReportDto>> GetRelatedPersonReportByRelationTypeReportAsync();
        Task<Person> GetByIdAsync(int id);
        Task UpdateAsync(Person person);
        Task UpdateRelatedPersonsAsync(int personId, IEnumerable<RelatedPersonDto> relatedPersonDtos);
    }
}