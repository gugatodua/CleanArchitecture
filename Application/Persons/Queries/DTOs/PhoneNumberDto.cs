using Domain.Enums;

namespace Application.Persons.Queries.DTOs
{
    public class PhoneNumberDto
    {
        public NumberType Type { get; set; }

        public string Number { get; set; }
    }
}