using Application.Persons.Queries.DTOs;
using MediatR;

namespace Application.Persons.Queries
{
    public class GetPersonQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }
    }
}
