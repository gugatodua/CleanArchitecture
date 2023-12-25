using System.Net;

namespace Application.CustomExceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public IDictionary<string, string[]> Errors { get; }

        public AppException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(IDictionary<string, string[]> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Validation failed")
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
