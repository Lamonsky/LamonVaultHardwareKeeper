using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;

namespace WebInterface.Controllers
{
    public class DeviceController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<DevicesVM> model = await RequestHelper.SendRequestAsync<object, List<DevicesVM>>(URLs.DEVICE_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }
        public async Task<IActionResult> PrintToPDF(int id)
        {
            ViewData["id"] = id;
            DevicesVM vm = await RequestHelper.SendRequestAsync<object, DevicesVM>(URLs.DEVICE_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM templatevm = templates.Where(item => item.Name == "Devices").FirstOrDefault();
            vm.Template = templatevm.HTMLContent
                .Replace("@Model.Name", vm.Name)
                .Replace("@Model.Manufacturer", vm.Manufacturer)
                .Replace("@Model.Status", vm.Status)
                .Replace("@Model.DeviceType", vm.DeviceType)
                .Replace("@Model.Model", vm.Model)                
                .Replace("@Model.SerialNumber", vm.SerialNumber)
                .Replace("@Model.InventoryNumber", vm.InventoryNumber)
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
