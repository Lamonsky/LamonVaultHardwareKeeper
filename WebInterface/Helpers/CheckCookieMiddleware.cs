using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebInterface.Helpers
{
    public class CheckCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var publicPaths = new[] { "/User/Login", "/User/Register", "/User/ForgotPassword" };
            var requestPath = context.Request.Path.Value;

            // Sprawdź, czy żądanie nie dotyczy strony publicznej
            if (!publicPaths.Contains(requestPath))
            {
                // Jeśli brak ciasteczka 'email', przekieruj do akcji 'Login'
                if (context.Request.Cookies["email"] == null)
                {
                    // Ręczne utworzenie ścieżki do akcji Login w kontrolerze User
                    string redirectUrl = "/User/Login"; // Możesz dostosować ten URL według swoich potrzeb

                    context.Response.Redirect(redirectUrl);
                    return;
                }
            }

            await _next(context);
        }
    }
}
