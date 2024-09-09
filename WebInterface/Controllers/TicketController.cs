using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;
using Data.Computers.CreateEditVMs;

namespace WebInterface.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<TicketVM> model = await RequestHelper.SendRequestAsync<object, List<TicketVM>>(URLs.TICKET_OWNER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
            return View(model);
        }
        public async Task<IActionResult> CreateNew()
        {
            List<TicketCategoryVM> ticketCategoryVMs = await RequestHelper.SendRequestAsync<object, List<TicketCategoryVM>>(URLs.TICKETCATEGORY, HttpMethod.Get, null, GlobalData.AccessToken);
            List<TicketStatusVM> ticketStatusVMs = await RequestHelper.SendRequestAsync<object, List<TicketStatusVM>>(URLs.TICKETSTATUS, HttpMethod.Get, null, GlobalData.AccessToken);
            List<TicketTypeVM> ticketTypeVMs = await RequestHelper.SendRequestAsync<object, List<TicketTypeVM>>(URLs.TICKETTYPE, HttpMethod.Get, null, GlobalData.AccessToken);
            List<LocationVM> locationVMs = await RequestHelper.SendRequestAsync<object, List<LocationVM>>(URLs.LOCATION, HttpMethod.Get, null, GlobalData.AccessToken);
            TicketCreateEditVM ticketCreateEditVM = new TicketCreateEditVM();
            TicketCreateNewViewModel model = new TicketCreateNewViewModel();
            model.TicketCategories = ticketCategoryVMs;
            model.TicketStatuses = ticketStatusVMs;
            model.TicketTypes = ticketTypeVMs;
            model.Ticket = ticketCreateEditVM;
            model.Location = locationVMs;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNew(TicketCreateEditVM vM)
        {
            await RequestHelper.SendRequestAsync(URLs.TICKET, HttpMethod.Post, vM, GlobalData.AccessToken);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
