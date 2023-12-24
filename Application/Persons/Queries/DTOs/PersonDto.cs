using Domain.Enums;

namespace Application.Persons.Queries.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalID { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();

        public string ImageURL { get; set; }

        public List<RelatedPersonDto> RelatedPeople { get; set; } = new List<RelatedPersonDto>();
    }
}
