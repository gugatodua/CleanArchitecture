using Application.Persons.Commands;
using Domain;
using MediatR;

namespace Application.Persons.CommandHandlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);

            if (person == null)
            {
                throw new Exception("Person not found");
            }

            await _personRepository.UpdateAsync(person);
            await _unitOfWork.CommitAsync();

            return person;
        }
    }
}