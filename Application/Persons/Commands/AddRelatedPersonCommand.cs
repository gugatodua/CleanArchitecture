using Application.Persons.Queries.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Persons.Commands
{
    public class AddRelatedPersonCommand : IRequest<Unit>
    {
        public int RelatedPersonIdentifier { get; set; }
        public RelationType RelationType { get; set; }
    }

    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddRelatedPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var relatedPersonDto = new RelatedPersonDto
                {
                    PersonId = request.RelatedPersonIdentifier,
                    RelationType = request.RelationType
                };

                await _personRepository.AddRelatedPerson(relatedPersonDto);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return Unit.Value;
        }
    }
}
