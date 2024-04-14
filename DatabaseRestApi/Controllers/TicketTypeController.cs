using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class TicketTypeController : Controller
    {
        [Route("/tickettype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketTypeVM> ticketTypeVM = await database.TicketTypes
                .Select(item => new TicketTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(ticketTypeVM);
        }
        [Route("/tickettype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketTypeCreateEditVM ticketTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.TicketTypes.Add(new()
            {
                Name = ticketTypeCreateEditVM.Name,
                Comment = ticketTypeCreateEditVM.Comment,
                Status = ticketTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/tickettype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TicketTypeCreateEditVM ticketTypeCreateEditVM)
        {
            DatabaseContext database = new();
            TicketType? ticketType = await database.TicketTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketType.Name = ticketTypeCreateEditVM.Name;
            ticketType.Comment = ticketTypeCreateEditVM.Comment;
            ticketType.Status = ticketTypeCreateEditVM.Status;
            ticketType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/tickettype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TicketTypeCreateEditVM ticketTypeCreateEditVM)
        {
            DatabaseContext database = new();
            TicketType? ticketType = await database.TicketTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketType.Status = 99;
            ticketType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
