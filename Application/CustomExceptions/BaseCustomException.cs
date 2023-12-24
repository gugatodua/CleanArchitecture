using System.Net;

namespace Application.CustomExceptions
{
    public class BaseCustomException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public BaseCustomException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
