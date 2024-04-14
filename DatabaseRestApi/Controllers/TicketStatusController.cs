using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class TicketStatusController : Controller
    {
        [Route("/ticketstatus")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketStatusVM> ticketStatusVM = await database.TicketStatuses
                .Select(item => new TicketStatusVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(ticketStatusVM);
        }
        [Route("/ticketstatus")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketStatusCreateEditVM ticketStatusCreateEditVM)
        {
            DatabaseContext database = new();
            database.TicketStatuses.Add(new()
            {
                Name = ticketStatusCreateEditVM.Name,
                Comment = ticketStatusCreateEditVM.Comment,
                Status = ticketStatusCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticketstatus/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TicketStatusCreateEditVM ticketStatusCreateEditVM)
        {
            DatabaseContext database = new();
            TicketStatus? ticketStatus = await database.TicketStatuses.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketStatus == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketStatus.Name = ticketStatusCreateEditVM.Name;
            ticketStatus.Comment = ticketStatusCreateEditVM.Comment;
            ticketStatus.Status = ticketStatusCreateEditVM.Status;
            ticketStatus.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticketstatus/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TicketStatusCreateEditVM ticketStatusCreateEditVM)
        {
            DatabaseContext database = new();
            TicketStatus? ticketStatus = await database.TicketStatuses.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketStatus == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketStatus.Status = 99;
            ticketStatus.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
