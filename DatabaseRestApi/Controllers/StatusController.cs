using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class StatusController : Controller
    {
        [Route(URLs.STATUS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<StatusVM> statusVM = await database.Statuses
                .Select(item => new StatusVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(statusVM);
        }
        [Route(URLs.STATUS_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            StatusVM statusVM = await database.Statuses
                .Where(item => item.Id == id)
                .Select(item => new StatusVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(statusVM);
        }
        [Route(URLs.STATUS_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetStatus(int id)
        {
            DatabaseContext database = new();
            StatusCreateEditVM statusVM = await database.Statuses
                .Where(item => item.Id == id)
                .Select(item => new StatusCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment
                    
                }).FirstAsync();
            return Json(statusVM);
        }
        [Route(URLs.STATUS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            database.Statuses.Add(new()
            {
                Name = statusCreateEditVM.Name,
                Comment = statusCreateEditVM.Comment,
                CreatedBy = statusCreateEditVM.CreatedBy,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.STATUS_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            Status? statuss = await database.Statuses.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            statuss.Name = statusCreateEditVM.Name;
            statuss.Comment = statusCreateEditVM.Comment;
            statuss.ModifiedBy = statusCreateEditVM.ModifiedBy;
            statuss.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.STATUS_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            Status? statuss = await database.Statuses.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            database.Statuses.Remove(statuss);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
