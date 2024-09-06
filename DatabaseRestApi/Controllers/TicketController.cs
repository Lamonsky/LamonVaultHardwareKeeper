using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class TicketController : Controller
    {
        [Route(URLs.TICKET)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketVM> ticketVM = await database.Tickets
                .Where(item => item.Status.CountToClosed == false)
                .Where(item => item.StatusId != 99)
                .OrderBy(item => item.StatusId)
                .Select(item => new TicketVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.TypeNavigation.Name,
                    Category = item.CategoryNavigation.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    User = item.UserNavigation.FirstName + " " + item.UserNavigation.LastName,
                    Owner = item.OwnerNavigation.Users.FirstName + " " + item.OwnerNavigation.Users.LastName,
                    Email = item.UserNavigation.Email,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(ticketVM);
        }
        [Route(URLs.CLOSEDTICKET)]
        [HttpGet]
        public async Task<IActionResult> ClosedTickets()
        {
            DatabaseContext database = new();
            List<TicketVM> ticketVM = await database.Tickets
                .Where(item => item.Status.CountToClosed == true)
                .Where(item => item.StatusId != 99)
                .OrderBy(item => item.StatusId)
                .Select(item => new TicketVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.TypeNavigation.Name,
                    Category = item.CategoryNavigation.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    User = item.UserNavigation.FirstName + " " + item.UserNavigation.LastName,
                    Owner = item.OwnerNavigation.Users.FirstName + " " + item.OwnerNavigation.Users.LastName,
                    Email = item.UserNavigation.Email,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(ticketVM);
        }
        [Route(URLs.TICKET_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TicketVM ticketVM = await database.Tickets
                .Where(item => item.Id == id)
                .Select(item => new TicketVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.TypeNavigation.Name,
                    Category = item.CategoryNavigation.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    User = item.UserNavigation.FirstName + " " + item.UserNavigation.LastName,
                    Owner = item.OwnerNavigation.Users.FirstName + " " + item.OwnerNavigation.Users.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(ticketVM);
        }
        [Route(URLs.TICKET_OWNER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItemByOwner(int id)
        {
            DatabaseContext database = new();
            List<TicketVM> ticketVM = await database.Tickets
                .Where(item => item.Owner == id)
                .Select(item => new TicketVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.TypeNavigation.Name,
                    Category = item.CategoryNavigation.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    User = item.UserNavigation.FirstName + " " + item.UserNavigation.LastName,
                    Owner = item.OwnerNavigation.Users.FirstName + " " + item.OwnerNavigation.Users.LastName,
                    Email = item.UserNavigation.Email,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(ticketVM);
        }
        [Route(URLs.TICKET_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            TicketCreateEditVM ticketVM = await database.Tickets
                .Where(item => item.Id == id)
                .Select(item => new TicketCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.Type,
                    Category = item.Category,
                    StatusId = item.StatusId,
                    LocationId = item.LocationId,
                    User = item.User,
                    Owner = item.Owner,

                }).FirstAsync();
            return Json(ticketVM);
        }
        [Route(URLs.TICKET)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TicketCreateEditVM ticketCreateEditVM)
        {
            DatabaseContext database = new();
            database.Tickets.Add(new()
            {
                Name = ticketCreateEditVM.Name,
                Type = ticketCreateEditVM.Type,
                Category = ticketCreateEditVM.Category,
                StatusId = ticketCreateEditVM.StatusId,
                LocationId = ticketCreateEditVM.LocationId,
                User = ticketCreateEditVM.User,
                Owner = ticketCreateEditVM.Owner,
                CreatedBy = ticketCreateEditVM.CreatedBy,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route(URLs.TICKET_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TicketCreateEditVM ticketCreateEditVM)
        {
            DatabaseContext database = new();
            Ticket? ticket = await database.Tickets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (ticket == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticket.Name = ticketCreateEditVM.Name;
            ticket.Type = ticketCreateEditVM.Type;
            ticket.Category = ticketCreateEditVM.Category;
            ticket.StatusId = ticketCreateEditVM.StatusId;
            ticket.LocationId = ticketCreateEditVM.LocationId;
            ticket.User = ticketCreateEditVM.User;
            ticket.Owner = ticketCreateEditVM.Owner;
            ticket.ModifiedAt = DateTime.Now;
            ticket.ModifiedBy = ticketCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.TICKET_ID)]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, [FromBody] TicketCreateEditVM ticketCreateEditVM)
        {
            DatabaseContext database = new();
            Ticket? ticket = await database.Tickets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (ticket == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticket.StatusId = 99;
            ticket.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
