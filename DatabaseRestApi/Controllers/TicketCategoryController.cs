using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class TicketCategoryController : Controller
    {
        [Route("/ticketcategory")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketCategoryVM> ticketCategoryVM = await database.TicketCategories
                .Select(item => new TicketCategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(ticketCategoryVM);
        }
        [Route("/ticketcategory")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            database.TicketCategories.Add(new()
            {
                Name = ticketCategoryCreateEditVM.Name,
                Comment = ticketCategoryCreateEditVM.Comment,
                Status = ticketCategoryCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticketcategory/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            TicketCategory? ticketCategory = await database.TicketCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketCategory.Name = ticketCategoryCreateEditVM.Name;
            ticketCategory.Comment = ticketCategoryCreateEditVM.Comment;
            ticketCategory.Status = ticketCategoryCreateEditVM.Status;
            ticketCategory.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/ticketcategory/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            TicketCategory? ticketCategory = await database.TicketCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketCategory.Status = 99;
            ticketCategory.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
