using Data;
using Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebInterface.Helpers;
using WebInterface.Models;
using Data.Computers.SelectVMs;
using Data.Computers.CreateEditVMs;
using System.Text.Json;

namespace WebInterface.Controllers
{
    public class TicketController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<TicketVM> model = await RequestHelper.SendRequestAsync<object, List<TicketVM>>(URLs.TICKET_USER_ID.Replace("{id}", GlobalData.Id.ToString()), HttpMethod.Get, null, GlobalData.AccessToken);
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
        public async Task<IActionResult> CreateNew(TicketCreateNewViewModel vM)
        {
            vM.Ticket.User = GlobalData.Id;
            await RequestHelper.SendRequestAsync(URLs.TICKET, HttpMethod.Post, vM.Ticket, GlobalData.AccessToken);

            string newitem = JsonSerializer.Serialize(vM.Ticket);
            LogCreateEditVM log = new();
            log.LogDate = DateTime.Now;
            log.Users = (int)GlobalData.Id;
            log.CreatedAt = DateTime.Now;
            log.CreatedBy = (int)GlobalData.Id;
            log.Description = $"Utworzono rekord w tabeli Zg³oszenia. Nowy rekord {newitem}.";
            await RequestHelper.SendRequestAsync(URLs.LOG, HttpMethod.Post, log, GlobalData.AccessToken);

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
