using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Persons.Commands
{
    public class UploadPictureCommand : IRequest<Unit>
    {
        public int PersonId { get; set; }
        public IFormFile Picture { get; set; }
    }
}
