using Application.CustomExceptions;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Net;
using TBCInterviewProject.Api.Resources;

namespace TBCInterviewProject.Api.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _requestDelegate;
        public ErrorHandler(RequestDelegate next)
        {
            this._requestDelegate = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandler> logger, IStringLocalizer<ErrorResources> localizer)
        {
            try
            {
                await _requestDelegate(context);
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

            if (ex is BaseCustomException)
            {
                var baseException = ((BaseCustomException)ex);

                statusCode = baseException.StatusCode;
                message = ex.Message;
            }
            else
            {
                logger.LogError(ex, ex.Message);
            }
            
            var result = JsonConvert.SerializeObject(message, new JsonSerializerSettings());
           
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}
