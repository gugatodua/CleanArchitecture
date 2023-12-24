using Domain;
using Domain.Enums;
using MediatR;

namespace Application.Persons.Commands
{
    public class CreatePersonCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}