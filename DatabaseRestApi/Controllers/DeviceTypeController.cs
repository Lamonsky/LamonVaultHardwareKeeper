using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.CodeAnalysis;

namespace DatabaseRestApi.Controllers
{
    public class DeviceTypeController : Controller
    {
        [Route(URLs.DEVICETYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<DeviceTypeVM> deviceTypeVM = await database.DeviceTypes
                .Select(item => new DeviceTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(deviceTypeVM);
        }
        [Route(URLs.DEVICETYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceTypeCreateEditVM deviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.DeviceTypes.Add(new()
            {
                Name = deviceTypeCreateEditVM.Name,
                Comment = deviceTypeCreateEditVM.Comment,
                Status = deviceTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICETYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] DeviceTypeCreateEditVM deviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            DeviceType? deviceType = await database.DeviceTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (deviceType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            deviceType.Name = deviceTypeCreateEditVM.Name;
            deviceType.Comment = deviceTypeCreateEditVM.Comment;
            deviceType.Status = deviceTypeCreateEditVM.Status;
            deviceType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICETYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] DeviceTypeCreateEditVM deviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            DeviceType? deviceType = await database.DeviceTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (deviceType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            deviceType.Status = 99;
            deviceType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
