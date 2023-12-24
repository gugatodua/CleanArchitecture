using System.Net;

namespace Application.CustomExceptions
{
    public class PersonNotFoundException : BaseCustomException
    {
        public PersonNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}
