using Application;
using Application.CustomExceptions;
using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly TbcDbContext _tbcDbContext;
        private readonly IStringLocalizer _localizer;

        public PersonRepository(TbcDbContext tbcDbContext, IStringLocalizer localizer)
        {
            _tbcDbContext = tbcDbContext;
            _localizer = localizer;
        }

        public async Task CreateAsync(Person person)
        {
            await _tbcDbContext.Persons.AddAsync(person);
        }

        public void Delete(Person person)
        {
            _tbcDbContext.RemoveRange(person.PhoneNumbers);
            _tbcDbContext.RemoveRange(person.RelatedPeople);
            _tbcDbContext.Remove(person);
        }

        public async Task<IEnumerable<Person>> GetAllAsync(Person person)
        {
            return await _tbcDbContext.Persons.ToArrayAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _tbcDbContext.Persons.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Person person)
        {
            _tbcDbContext.Entry(person).State = EntityState.Modified;
        }

        public async Task UpdateRelatedPersonsAsync(int personId, IEnumerable<RelatedPersonDto> relatedPersonDtos)
        {
            var person = await _tbcDbContext.Persons
                .Include(x => x.RelatedPeople)
                .SingleOrDefaultAsync(x => x.Id == personId);

            var errorMessageKey = "PersonNotFound";
            var message = _localizer[errorMessageKey].ToString();

            if (person == null)
            {
                throw new PersonNotFoundException(System.Net.HttpStatusCode.NotFound, message);
            }

            person.RelatedPeople.Clear();

            foreach (var dto in relatedPersonDtos)
            {
                var relatedPerson = new RelatedPerson
                {
                    RelatedPersonIdentifier = dto.RelatedPersonIdentifier,
                    RelationType = dto.RelationType,
                };
                person.RelatedPeople.Add(relatedPerson);
            }

            _tbcDbContext.Entry(person).State = EntityState.Modified;
        }

        public async Task<IEnumerable<RelatedPersonReportDto>> GetRelatedPersonReportByRelationTypeReportAsync()
        {
            return await _tbcDbContext.Persons.Select(p => new RelatedPersonReportDto
            {
                PersonId = p.Id,
                RelationCounts = p.RelatedPeople
                          .GroupBy(rp => rp.RelationType)
                          .Select(g => new RelationCount
                          {
                              RelationType = g.Key,
                              Count = g.Count()
                          }).ToList()
            }).ToListAsync();
        }
    }
}