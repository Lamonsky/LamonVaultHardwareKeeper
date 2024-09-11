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
    public class TicketTypeController : Controller
    {
        [Authorize]
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
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(ticketTypeVM);
        }
        [Authorize]
        [Route(URLs.TICKETTYPE_ID)]
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
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(ticketTypeVM);
        }
        [Authorize]
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
                    Status = item.Status,

                }).FirstAsync();
            return Json(ticketTypeVM);
        }
        [Authorize(Roles = "Admin")]
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
                CreatedAt = DateTime.Now,
                CreatedBy = ticketTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
            ticketType.ModifiedBy = ticketTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
