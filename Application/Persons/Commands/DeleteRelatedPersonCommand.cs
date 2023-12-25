using Application.CustomExceptions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Persons.Commands
{
    public class DeleteRelatedPersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteRelatedPersonCommandHandler : IRequestHandler<DeleteRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public DeleteRelatedPersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<Unit> Handle(DeleteRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var relatedPerson = await _personRepository.GetRelatedPersonById(request.Id);

                if (relatedPerson == null)
                {
                    throw new PersonNotFoundException(System.Net.HttpStatusCode.NotFound, _localizer["RelatedPersonNotFound"]);
                }

                _personRepository.DeleteRelatedPerson(relatedPerson);
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
