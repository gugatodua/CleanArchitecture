using Application.Persons.Queries.DTOs;
using AutoMapper;
using Domain;

namespace Application.Persons
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<RelatedPerson, RelatedPersonDto>();
            CreateMap<PhoneNumber, PhoneNumberDto>();
        }
    }
}
