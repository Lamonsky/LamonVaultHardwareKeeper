using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebInterface.Models;

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

            if (!publicPaths.Contains(requestPath))
            {
                if (context.Request.Cookies["email"] == null)
                {                    
                    string redirectUrl = "/User/Login";

                    context.Response.Redirect(redirectUrl);
                    return;
                }
            }
            GlobalData.RefreshUserToken();
            await _next(context);
        }
    }
}
