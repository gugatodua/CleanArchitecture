using Domain.Enums;

namespace Domain
{
    public class PhoneNumber
    {
        public NumberType Type { get; set; }

        public string Number { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
