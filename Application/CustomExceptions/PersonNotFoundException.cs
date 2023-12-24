using System.Net;

namespace Application.CustomExceptions
{
    public class PersonNotFoundException : ApplicationException
    {
        public PersonNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}
