using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class RelatedPerson
    {
        public int RelatedPersonIdentifier { get; set; }
        
        [Required]
        public RelationType RelationType { get; set; }
    }
}
