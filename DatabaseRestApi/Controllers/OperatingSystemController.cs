using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class OperatingSystemController : Controller
    {
        [Route(URLs.OS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<OperatingSystemVM> operaingSystemVM = await database.OperatingSystems
                .Select(item => new OperatingSystemVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(operaingSystemVM);
        }
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

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
            operaingSystems.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

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
