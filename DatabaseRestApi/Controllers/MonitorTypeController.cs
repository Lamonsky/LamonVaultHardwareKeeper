using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class MonitorTypeController : Controller
    {
        [Route("/monitortype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<MonitorTypeVM> monitorTypeVM = await database.MonitorTypes
                .Select(item => new MonitorTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(monitorTypeVM);
        }
        [Route("/monitortype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonitorTypeCreateEditVM monitorTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.MonitorTypes.Add(new()
            {
                Name = monitorTypeCreateEditVM.Name,
                Comment = monitorTypeCreateEditVM.Comment,
                Status = monitorTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/monitortype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] MonitorTypeCreateEditVM monitorTypeCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorType? monitorType = await database.MonitorTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorType.Name = monitorTypeCreateEditVM.Name;
            monitorType.Comment = monitorTypeCreateEditVM.Comment;
            monitorType.Status = monitorTypeCreateEditVM.Status;
            monitorType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/monitortype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] MonitorTypeCreateEditVM monitorTypeCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorType? monitorType = await database.MonitorTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorType.Status = 99;
            monitorType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
