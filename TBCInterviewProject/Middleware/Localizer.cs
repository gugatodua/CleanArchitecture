namespace TBCInterviewProject.Api.Middleware
{
    public class Localizer
    {
        private readonly RequestDelegate _requestDelegate;

        public Localizer(RequestDelegate next)
        {
            _requestDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userLanguage = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
            var defaultLanguage = string.IsNullOrEmpty(userLanguage) ? "en" : userLanguage;

            await _requestDelegate.Invoke(context);
        }
    }
}