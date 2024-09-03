using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class MonitorTypeController : Controller
    {
        [Route(URLs.MONITORTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<MonitorTypeVM> monitorTypeVM = await database.MonitorTypes
                .Where(item => item.Status != 99)
                .Select(item => new MonitorTypeVM
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
            return Json(monitorTypeVM);
        }
        [Route(URLs.MONITORTYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            MonitorTypeVM monitorTypeVM = await database.MonitorTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new MonitorTypeVM
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
            return Json(monitorTypeVM);
        }
        [Route(URLs.MONITORTYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            MonitorTypeCreateEditVM monitorTypeVM = await database.MonitorTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new MonitorTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(monitorTypeVM);
        }
        [Route(URLs.MONITORTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonitorTypeCreateEditVM monitorTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.MonitorTypes.Add(new()
            {
                Name = monitorTypeCreateEditVM.Name,
                Comment = monitorTypeCreateEditVM.Comment,
                Status = monitorTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = monitorTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MONITORTYPE_ID)]
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
            monitorType.ModifiedBy = monitorTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MONITORTYPE_ID)]
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
