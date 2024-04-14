using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class MonitorModelController : Controller
    {
        [Route("/monitormodel")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<MonitorModelVM> monitorModelVM = await database.MonitorModels
                .Select(item => new MonitorModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(monitorModelVM);
        }
        [Route("/monitormodel")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.MonitorModels.Add(new()
            {
                Name = monitorModelCreateEditVM.Name,
                Comment = monitorModelCreateEditVM.Comment,
                Status = monitorModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/monitormodel/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorModel? monitorModels = await database.MonitorModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorModels.Name = monitorModelCreateEditVM.Name;
            monitorModels.Comment = monitorModelCreateEditVM.Comment;
            monitorModels.Status = monitorModelCreateEditVM.Status;
            monitorModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/monitormodel/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorModel? monitorModels = await database.MonitorModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorModels.Status = 99;
            monitorModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
