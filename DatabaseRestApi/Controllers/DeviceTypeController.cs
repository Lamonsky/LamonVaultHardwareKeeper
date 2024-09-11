using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class DeviceTypeController : Controller
    {
        [Authorize]
        [Route(URLs.DEVICETYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<DeviceTypeVM> deviceTypeVM = await database.DeviceTypes
                .Where(item => item.Status != 99)
                .Select(item => new DeviceTypeVM
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
            return Json(deviceTypeVM);
        }
        [Authorize]
        [Route(URLs.DEVICETYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            DeviceTypeVM deviceTypeVM = await database.DeviceTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new DeviceTypeVM
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
            return Json(deviceTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.DEVICETYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            DeviceTypeCreateEditVM deviceTypeVM = await database.DeviceTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new DeviceTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(deviceTypeVM);
        }
        [Authorize(Roles = "Admin")]
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
                CreatedAt = DateTime.Now,
                CreatedBy = deviceTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
            deviceType.ModifiedBy = deviceTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
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
