using Application.CustomExceptions;
using Application.Persons.Queries;
using Application.Persons.Queries.DTOs;
using Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Application.Persons.QueryHandlers
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer _localizer;
        public GetPersonQueryHandler(IPersonRepository personRepository, IStringLocalizer localizer)
        {
            _personRepository = personRepository;
            _localizer = localizer;
        }

        public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);

            var errorMessageKey = "PersonNotFound";
            var message = _localizer[errorMessageKey].ToString();

            if (person == null)
            {
                throw new PersonNotFoundException(HttpStatusCode.NotFound, message);
            }

            return new PersonDto
            {
                Id = person.Id,
                BirthDate = person.BirthDate,
                CityId = person.CityId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                ImageURL = person.ImageURL,
                PersonalID = person.PersonalID,
                PhoneNumbers = MapPhoneNumbers(person.PhoneNumbers),
                RelatedPeople = MapRelatedPeople(person.RelatedPeople)
            };
        }

        private List<PhoneNumberDto> MapPhoneNumbers(List<PhoneNumber> phoneNumbers)
        {
            var phoneNumberDtos = new List<PhoneNumberDto>();

            foreach (var phoneNumber in phoneNumbers)
            {
                var phoneNumberDto = new PhoneNumberDto
                {
                    Type = phoneNumber.Type,
                    Number = phoneNumber.Number
                };

                phoneNumberDtos.Add(phoneNumberDto);
            }

            return phoneNumberDtos;
        }

        private List<RelatedPersonDto> MapRelatedPeople(List<RelatedPerson> relatedPersons)
        {
            var result = new List<RelatedPersonDto>();

            foreach (var relatedPerson in relatedPersons)
            {
                var relatedPersonDto = new RelatedPersonDto
                {
                    RelatedPersonIdentifier = relatedPerson.RelatedPersonIdentifier,
                    RelationType = relatedPerson.RelationType
                };

                result.Add(relatedPersonDto);
            }

            return result;
        }
    }
}
