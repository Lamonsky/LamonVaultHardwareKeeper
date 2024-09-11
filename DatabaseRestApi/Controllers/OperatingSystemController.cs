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
    public class OperatingSystemController : Controller
    {
        [Authorize]
        [Route(URLs.OS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<OperatingSystemVM> operaingSystemVM = await database.OperatingSystems
                .Where(item => item.Status != 99)
                .Select(item => new OperatingSystemVM
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
            return Json(operaingSystemVM);
        }
        [Authorize]
        [Route(URLs.OS_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            OperatingSystemVM operaingSystemVM = await database.OperatingSystems
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new OperatingSystemVM
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
            return Json(operaingSystemVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.OS_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            OperatingSystemCreateEditVM operaingSystemVM = await database.OperatingSystems
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new OperatingSystemCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(operaingSystemVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.OS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OperatingSystemCreateEditVM operaingSystemCreateEditVM)
        {
            DatabaseContext database = new();
            database.OperatingSystems.Add(new()
            {
                Name = operaingSystemCreateEditVM.Name,
                Comment = operaingSystemCreateEditVM.Comment,
                Status = operaingSystemCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = operaingSystemCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.OS_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] OperatingSystemCreateEditVM operaingSystemCreateEditVM)
        {
            DatabaseContext database = new();
            Models.OperatingSystem? operaingSystems = await database.OperatingSystems.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (operaingSystems == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            operaingSystems.Name = operaingSystemCreateEditVM.Name;
            operaingSystems.Comment = operaingSystemCreateEditVM.Comment;
            operaingSystems.Status = operaingSystemCreateEditVM.Status;
            operaingSystems.ModifiedBy = operaingSystemCreateEditVM.ModifiedBy;
            operaingSystems.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.OS_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] OperatingSystemCreateEditVM operaingSystemCreateEditVM)
        {
            DatabaseContext database = new();
            Models.OperatingSystem? operaingSystems = await database.OperatingSystems.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (operaingSystems == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            operaingSystems.Status = 99;
            operaingSystems.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
