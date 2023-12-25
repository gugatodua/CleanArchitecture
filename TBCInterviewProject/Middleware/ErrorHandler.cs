using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Net;
using TBCInterviewProject.Api.Resources;

namespace TBCInterviewProject.Api.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        public ErrorHandler(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandler> logger, IStringLocalizer<ErrorResources> localizer)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex, logger, localizer);
            }
        }

        private static Task HandleAsync(HttpContext context, Exception ex, ILogger<ErrorHandler> logger, IStringLocalizer<ErrorResources> localizer)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errorMessageKey = "InternalServerError";

            var message = localizer[errorMessageKey].ToString();

            switch (ex)
            {
                case Application.CustomExceptions.PersonNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessageKey = "PersonNotFound";
                    message = localizer[errorMessageKey].ToString();
                    break;

                case Application.CustomExceptions.FileUploadException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessageKey = "FileUploadError";
                    message = localizer[errorMessageKey].Value;
                    break;

                case Application.CustomExceptions.AppException appEx:
                    statusCode = appEx.StatusCode;
                    message = ex.Message;
                    break;

                default:
                    logger.LogError(ex, ex.Message);
                    message = localizer[errorMessageKey].Value;
                    break;
            }

            var result = JsonConvert.SerializeObject(message, new JsonSerializerSettings());
           
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}
