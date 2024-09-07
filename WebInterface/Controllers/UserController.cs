using WebInterface.Helpers;
using WebInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Computers.SelectVMs;
using Data;

namespace WebInterface.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            Response.Cookies.Delete("email");
            GlobalData.AccessToken = null;
            GlobalData.RefreshToken = null;
            GlobalData.Email = null;
            GlobalData.Id = null;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {            
            try
            {
                LoginResponse loginResponse = await RequestHelper.SendRequestAsync<LoginRequest, LoginResponse>(URLs.LOGIN, HttpMethod.Post, loginRequest, GlobalData.AccessToken);
                Response.Cookies.Append("email", loginRequest.EMail);
                UserVM loggeduser = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_EMAIL_ID.Replace("{id}", loginRequest.EMail), HttpMethod.Get, null, GlobalData.AccessToken);
                GlobalData.AccessToken = loginResponse.AccessToken;
                GlobalData.RefreshToken = loginResponse.RefreshToken;
                GlobalData.Email = loginResponse.Email;
                GlobalData.Id = loggeduser.Id;
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                return View("ErrorView");
            }
            
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("email");
            GlobalData.AccessToken = null;
            GlobalData.RefreshToken = null;
            GlobalData.Email = null;
            GlobalData.Id = null;
            return RedirectToAction("Login");
        }
    }
}
