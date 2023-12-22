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
        [MaxLength(11)]
        [MinLength(11)]
        public string PersonalID { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        //TODO : City list?
        public string City { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public string ImageAddressPath { get; set; }

        public List<RelatedPerson> RelatedPeople { get; set; } = new List<RelatedPerson>();
    }
}
