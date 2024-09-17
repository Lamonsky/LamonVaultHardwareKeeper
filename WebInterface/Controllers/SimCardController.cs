using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;

namespace WebInterface.Controllers
{
    public class SimCardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<SimCardsVM> model = await RequestHelper.SendRequestAsync<object, List<SimCardsVM>>(URLs.SIMCARD_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }
        public async Task<IActionResult> PrintToPDF(int id)
        {
            ViewData["id"] = id;
            SimCardsVM vm = await RequestHelper.SendRequestAsync<object, SimCardsVM>(URLs.SIMCARD_ID.Replace("{id}", id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            List<TemplatesVM> templates = await RequestHelper.SendRequestAsync<object, List<TemplatesVM>>(URLs.TEMPLATE, HttpMethod.Get, null, GlobalData.AccessToken);
            TemplatesVM tvm = templates.Where(item => item.Name == "SimCards").FirstOrDefault();
            vm.Template = tvm.HTMLContent
                .Replace("@Model.PinCode", vm.PinCode)
                .Replace("@Model.PukCode", vm.PukCode)
                .Replace("@Model.Component", vm.Component)
                .Replace("@Model.PhoneNumber", vm.PhoneNumber)
                .Replace("@Model.Status", vm.Status)                
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
