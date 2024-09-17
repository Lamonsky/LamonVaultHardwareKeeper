using Data;
using Data.Computers.SelectVMs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;

namespace WebInterface.Controllers
{
    public class ComputerController : Controller
    {

        public async Task<IActionResult> Index()
        {
            List<ComputersVM> model = await RequestHelper.SendRequestAsync<object, List<ComputersVM>>(URLs.COMPUTERS_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }

        public async Task<IActionResult> PrintToPDF(int id)
        {
            ViewData["id"] = id;
            ComputersVM vm = await RequestHelper.SendRequestAsync<object, ComputersVM>(URLs.COMPUTERS_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM tvm = templates.Where(item => item.Name == "Computers").FirstOrDefault();
            vm.Template = tvm.HTMLContent
                .Replace("@Model.Name", vm.Name)
                .Replace("@Model.ManufacturerName", vm.ManufacturerName)
                .Replace("@Model.Location", vm.Location)
                .Replace("@Model.Status", vm.Status)
                .Replace("@Model.ComputerType", vm.ComputerType)
                .Replace("@Model.ComputerModel", vm.ComputerModel)
                .Replace("@Model.Processor", vm.Processor)
                .Replace("@Model.Ram", vm.Ram)
                .Replace("@Model.Disk", vm.Disk)
                .Replace("@Model.GraphicsCard", vm.GraphicsCard)
                .Replace("@Model.OperatingSystem", vm.OperatingSystem)
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
