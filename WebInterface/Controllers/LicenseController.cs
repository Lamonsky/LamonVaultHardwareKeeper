using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;

namespace WebInterface.Controllers
{
    public class LicenseController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<LicenseVM> model = await RequestHelper.SendRequestAsync<object, List<LicenseVM>>(URLs.LICENSE_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }
        public async Task<IActionResult> PrintToPDF(int id)
        {
            ViewData["id"] = id;
            LicenseVM vm = await RequestHelper.SendRequestAsync<object, LicenseVM>(URLs.LICENSE_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM tvm = templates.Where(item => item.Name == "Licenses").FirstOrDefault();
            vm.Template = tvm.HTMLContent
                .Replace("@Model.Software", vm.Software)
                .Replace("@Model.Name", vm.Name)
                .Replace("@Model.LicenseType", vm.LicenseType)
                .Replace("@Model.Publisher", vm.Publisher)
                .Replace("@Model.Status", vm.Status)
                .Replace("@Model.Location", vm.Location)                
                .Replace("@Model.SerialNumber", vm.SerialNumber)
                .Replace("@Model.InventoryNumber", vm.InventoryNumber)
                .Replace("@Model.User", vm.User);
            return View(vm);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
