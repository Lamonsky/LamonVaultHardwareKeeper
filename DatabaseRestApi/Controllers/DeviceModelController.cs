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
                .Where(item => item.Status != 99)
                .Select(item => new DeviceModelVM
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
            return Json(deviceModelVM);
        }
        [Route(URLs.DEVICEMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            DeviceModelVM deviceModelVM = await database.DeviceModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new DeviceModelVM
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
            return Json(deviceModelVM);
        }
        [Route(URLs.DEVICEMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            DeviceModelCreateEditVM deviceModelVM = await database.DeviceModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new DeviceModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status
                    
                }).FirstAsync();
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
                CreatedAt = DateTime.Now,
                CreatedBy = deviceModelCreateEditVM.CreatedBy,
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
            deviceModels.ModifiedBy = deviceModelCreateEditVM.ModifiedBy;
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
