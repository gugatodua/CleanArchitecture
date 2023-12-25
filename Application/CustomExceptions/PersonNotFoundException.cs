using System.Net;

namespace Application.CustomExceptions
{
    public class PersonNotFoundException : AppException
    {
        public PersonNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}
