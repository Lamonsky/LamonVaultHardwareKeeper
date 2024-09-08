using Data;
using Data.Computers.SelectVMs;
using Data.Helpers;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
