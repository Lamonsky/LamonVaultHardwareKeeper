using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class TicketTypeController : Controller
    {
        [Route(URLs.TICKETTYPE)]
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
        }[Route(URLs.TICKETTYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TicketTypeVM ticketTypeVM = await database.TicketTypes
                .Where(item => item.Id == id)
                .Select(item => new TicketTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).FirstAsync();
            return Json(ticketTypeVM);
        }
        [Route(URLs.TICKETTYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            TicketTypeCreateEditVM ticketTypeVM = await database.TicketTypes
                .Where(item => item.Id == id)
                .Select(item => new TicketTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(ticketTypeVM);
        }
        [Route(URLs.TICKETTYPE)]
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

        [Route(URLs.TICKETTYPE_ID)]
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

        [Route(URLs.TICKETTYPE_ID)]
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
