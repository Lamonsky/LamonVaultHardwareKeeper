using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class TicketStatusController : Controller
    {
        [Route(URLs.TICKETSTATUS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketStatusVM> ticketStatusVM = await database.TicketStatuses
                .Where(item => item.Status != 99)
                .Select(item => new TicketStatusVM
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
            return Json(ticketStatusVM);
        }
        [Route(URLs.TICKETSTATUS_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TicketStatusVM ticketStatusVM = await database.TicketStatuses
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TicketStatusVM
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
            return Json(ticketStatusVM);
        }
        [Route(URLs.TICKETSTATUS_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEdititem(int id)
        {
            DatabaseContext database = new();
            TicketStatusCreateEditVM ticketStatusVM = await database.TicketStatuses
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TicketStatusCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(ticketStatusVM);
        }
        [Route(URLs.TICKETSTATUS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketStatusCreateEditVM ticketStatusCreateEditVM)
        {
            DatabaseContext database = new();
            database.TicketStatuses.Add(new()
            {
                Name = ticketStatusCreateEditVM.Name,
                Comment = ticketStatusCreateEditVM.Comment,
                Status = ticketStatusCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = ticketStatusCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.TICKETSTATUS_ID)]
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
            ticketStatus.ModifiedBy = ticketStatusCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.TICKETSTATUS_ID)]
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
