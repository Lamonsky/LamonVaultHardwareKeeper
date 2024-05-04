using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class DeviceModelController : Controller
    {
        [Route(URLs.DEVICEMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<DeviceModelVM> deviceModelVM = await database.DeviceModels
                .Select(item => new DeviceModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                    
                }).ToListAsync();
            return Json(deviceModelVM);
        }
        [Route(URLs.DEVICEMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceModelCreateEditVM deviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.DeviceModels.Add(new()
            {
                Name = deviceModelCreateEditVM.Name,
                Comment = deviceModelCreateEditVM.Comment,
                Status = deviceModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICEMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] DeviceModelCreateEditVM deviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            DeviceModel? deviceModels = await database.DeviceModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (deviceModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            deviceModels.Name = deviceModelCreateEditVM.Name;
            deviceModels.Comment = deviceModelCreateEditVM.Comment;
            deviceModels.Status = deviceModelCreateEditVM.Status;
            deviceModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICEMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] DeviceModelCreateEditVM deviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            DeviceModel? deviceModels = await database.DeviceModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (deviceModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            deviceModels.Status = 99;
            deviceModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
