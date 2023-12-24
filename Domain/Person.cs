using Domain.Enums;

namespace Domain
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalId { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string ImageURL { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public List<RelatedPerson> RelatedPeople { get; set; } = new List<RelatedPerson>();
    }
}