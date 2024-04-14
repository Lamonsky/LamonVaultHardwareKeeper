using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class TicketController : Controller
    {
        [Route("/ticket")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketVM> ticketVM = await database.Tickets
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

                }).ToListAsync();
            return Json(ticketVM);
        }
        [Route("/ticket")]
        [HttpPost]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticket/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticket/{id}")]
        [HttpDelete]
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
