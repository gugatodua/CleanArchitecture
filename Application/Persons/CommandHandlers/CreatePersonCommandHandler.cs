using Application.Persons.Commands;
using MediatR;

namespace Application.Persons.CommandHandlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Domain.Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                PhoneNumbers = request.PhoneNumbers,
                CityId = request.CityId
            };

            await _personRepository.CreateAsync(person);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}