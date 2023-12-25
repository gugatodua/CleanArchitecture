using Application.CustomExceptions;
using Application.Persons;
using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly TbcDbContext _tbcDbContext;
        private readonly IStringLocalizer _localizer;
        private readonly IMapper _mapper;

        public PersonRepository(TbcDbContext tbcDbContext, IStringLocalizer localizer, IMapper mapper)
        {
            _tbcDbContext = tbcDbContext;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task CreateAsync(Person person)
        {
            await _tbcDbContext.Persons.AddAsync(person);
        }

        public async void Delete(Person person)
        {
            _tbcDbContext.RemoveRange(person.PhoneNumbers);
            _tbcDbContext.RemoveRange(person.RelatedPeople);
            _tbcDbContext.Remove(person);
        }

        public async Task<IEnumerable<Person>> GetAllAsync(Person person)
        {
            return await _tbcDbContext.Persons.ToArrayAsync();
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await GetPersonDbModelByIdAsync(id);

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<Person> GetPersonDbModelByIdAsync(int id)
        {
            return await _tbcDbContext.Persons
                .Include(x => x.PhoneNumbers)
                .Include(x => x.RelatedPeople)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async void Update(Person person)
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

            var relatedPeople = _mapper.Map<IEnumerable<RelatedPerson>>(relatedPersonDtos);

            person.RelatedPeople.AddRange(relatedPeople);

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

        public async Task<IEnumerable<PersonDto>> QuickSearchPerson(string? keyword)
        {
            var query = _tbcDbContext.Persons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.FirstName.Contains(keyword)
                                      || p.LastName.Contains(keyword)
                                      || p.PersonalId.Contains(keyword));
            }

            var persons = await query.ToListAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }

        public async Task<IEnumerable<PersonDto>> DetailSearchPerson(PersonDetailedSearchQuery detailedSearchQuery)
        {
            var query = _tbcDbContext.Persons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(detailedSearchQuery.FirstName))
            {
                query = query.Where(p => p.FirstName.Contains(detailedSearchQuery.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(detailedSearchQuery.LastName))
            {
                query = query.Where(p => p.LastName.Contains(detailedSearchQuery.LastName));
            }

            if (detailedSearchQuery.Gender.HasValue)
            {
                query = query.Where(p => p.Gender == detailedSearchQuery.Gender.Value);
            }

            if (!string.IsNullOrWhiteSpace(detailedSearchQuery.PersonalId))
            {
                query = query.Where(p => p.PersonalId == detailedSearchQuery.PersonalId);
            }

            if (detailedSearchQuery.BirthDate.HasValue)
            {
                query = query.Where(p => p.BirthDate.Date == detailedSearchQuery.BirthDate.Value.Date);
            }

            if (detailedSearchQuery.CityId.HasValue)
            {
                query = query.Where(p => p.CityId == detailedSearchQuery.CityId.Value);
            }

            var persons = await query.Skip((detailedSearchQuery.PageNumber - 1) * detailedSearchQuery.PageSize)
                            .Take(detailedSearchQuery.PageSize)
                            .ToListAsync();

            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }

        public async Task AddRelatedPerson(RelatedPersonDto relatedPersonDto)
        {
            var relatedPerson = _mapper.Map<RelatedPerson>(relatedPersonDto);

            var person = await GetPersonDbModelByIdAsync(relatedPersonDto.PersonId);
            if (person == null)
            {
                throw new PersonNotFoundException(System.Net.HttpStatusCode.NotFound, _localizer["PersonNotFound"]);
            }

            relatedPerson.Person = person;

            await _tbcDbContext.RelatedPersons.AddAsync(relatedPerson);
        }

        public async Task<RelatedPerson> GetRelatedPersonById(int id)
        {
            return await _tbcDbContext.RelatedPersons
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void DeleteRelatedPerson(RelatedPerson relatedPerson)
        {
            _tbcDbContext.Remove(relatedPerson);
        }
    }
}