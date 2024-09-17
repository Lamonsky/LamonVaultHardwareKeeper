using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;

namespace WebInterface.Controllers
{
    public class SoftwareController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<SoftwaresVM> model = await RequestHelper.SendRequestAsync<object, List<SoftwaresVM>>(URLs.SOFTWARE_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }
        public async Task<IActionResult> PrintToPDF(int id)
        {
            ViewData["id"] = id;
            SoftwaresVM vm = await RequestHelper.SendRequestAsync<object, SoftwaresVM>(URLs.SOFTWARE_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM tvm = templates.Where(item => item.Name == "Softwares").FirstOrDefault();
            vm.Template = tvm.HTMLContent
                .Replace("@Model.Name", vm.Name)
                .Replace("@Model.Location", vm.Location)
                .Replace("@Model.Status", vm.Status)
                .Replace("@Model.Publisher", vm.Publisher)               
                .Replace("@Model.Users", vm.Users);
            return View(vm);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
