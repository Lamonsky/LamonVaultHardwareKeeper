﻿using WebInterface.Helpers;
using WebInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Computers.SelectVMs;
using Data;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = WebInterface.Models.LoginRequest;

namespace WebInterface.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PrintToPDF()
        {
            ViewData["id"] = GlobalData.Id;
            UserVM vm = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM templatesVM = templates.Where(item => item.Name == "Users").FirstOrDefault();
            vm.Template = templatesVM.HTMLContent
                .Replace("@Model.Usersname", vm.Usersname)
                .Replace("@Model.FirstName", vm.FirstName)
                .Replace("@Model.LastName", vm.LastName)
                .Replace("@Model.Location", vm.Location)
                .Replace("@Model.Email", vm.Email)
                .Replace("@Model.Phone1", vm.Phone1)
                .Replace("@Model.Phone2", vm.Phone2)
                .Replace("@Model.InternalNumber", vm.InternalNumber)
                .Replace("@Model.Position", vm.Position);             
            return View(vm);
        }
        public async Task<IActionResult> Details()
        {
            UserVM model = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {            
            try
            {
                LoginResponse loginResponse = await RequestHelper.SendRequestAsync<LoginRequest, LoginResponse>(URLs.LOGIN, HttpMethod.Post, loginRequest, GlobalData.AccessToken);
                Response.Cookies.Append("email", loginRequest.EMail);
                GlobalData.AccessToken = loginResponse.AccessToken;
                GlobalData.RefreshToken = loginResponse.RefreshToken;
                GlobalData.Email = loginRequest.EMail;
                UserVM loggeduser = await RequestHelper.SendRequestAsync<object, UserVM>(URLs.USER_EMAIL_ID.Replace("{id}", loginRequest.EMail), HttpMethod.Get, null, GlobalData.AccessToken);
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
            Response.Cookies.Delete("accesstoken");
            Response.Cookies.Delete("refreshtoken");
            Response.Cookies.Delete("userid");
            return RedirectToAction("Login");
        }
    }
}
