using Domain.Enums;

namespace Domain
{
    public class RelatedPerson
    {
        public int Id { get; set; }
        
        public RelationType RelationType { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}