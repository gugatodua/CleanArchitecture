using Application.Persons.Commands;
using Application.Persons.Queries.DTOs;
using MediatR;

namespace Application.Persons.CommandHandlers
{
    public class UpdateRelatedPersonListCommandHandler : IRequestHandler<UpdateRelatedPersonListCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRelatedPersonListCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateRelatedPersonListCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.UpdateRelatedPersonsAsync(request.PersonId, request.RelatedPersons);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
