using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Zა-ჰ]+$", ErrorMessage = "First Name can only consist of Georgian or Latin letters")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Zა-ჰ]+$", ErrorMessage = "Last Name can only consist of Georgian or Latin letters")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal ID must be 11 characters long")]
        public string PersonalID { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public string ImageURL { get; set; }

        public List<RelatedPerson> RelatedPeople { get; set; } = new List<RelatedPerson>();
    }
}