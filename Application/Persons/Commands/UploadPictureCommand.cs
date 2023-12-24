using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Persons.Commands
{
    public class UploadPictureCommand : IRequest<Unit>
    {
        public int PersonId { get; set; }
        public IFormFile Picture { get; set; }
    }

    public class UploadPictureCommandHandler : IRequestHandler<UploadPictureCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UploadPictureCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(UploadPictureCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
            {
                throw new Exception("Person not found");
            }

            if (request.Picture == null || request.Picture.Length == 0)
            {
                throw new ArgumentException("No picture file provided");
            }

            var imageAddressPath = await _fileService.SaveFileAsync(request.Picture);

            person.ImageURL = imageAddressPath;

            _personRepository.Update(person);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}