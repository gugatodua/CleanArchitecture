using System.Net;

namespace Application.CustomExceptions
{
    public class ApplicationException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ApplicationException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
