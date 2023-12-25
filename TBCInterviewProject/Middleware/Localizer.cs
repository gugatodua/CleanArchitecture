using System.Globalization;

namespace TBCInterviewProject.Api.Middleware
{
    public class Localizer
    {
        private readonly RequestDelegate _next;

        public Localizer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userLanguage = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            var culture = string.IsNullOrEmpty(userLanguage) ? "en-US" : userLanguage;

            var defaultLanguage = string.IsNullOrEmpty(userLanguage) ? "en-US" : userLanguage;
            if (!string.IsNullOrEmpty(defaultLanguage))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(defaultLanguage);
                    CultureInfo.CurrentUICulture = new CultureInfo(defaultLanguage);
                }
                catch (CultureNotFoundException)
                {
                }
            }
            await _next(context);
        }
    }
}