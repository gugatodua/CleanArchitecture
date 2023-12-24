using MediatR;

namespace Application.Persons.Commands
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
