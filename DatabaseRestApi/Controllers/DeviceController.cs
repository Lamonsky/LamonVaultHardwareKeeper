using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace DatabaseRestApi.Controllers
{
    public class DeviceController : Controller
    {
        [Route(URLs.DEVICE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<DevicesVM> devicesVMs = await database.Devices
                .Where(item => item.StatusId != 99)
                .Select(item => new DevicesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(devicesVMs);
        }
        [Route(URLs.DEVICE_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<DevicesVM> devicesVMs = await database.Devices
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new DevicesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(devicesVMs);
        }
        [Route(URLs.DEVICE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            DevicesVM devicesVMs = await database.Devices
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new DevicesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(devicesVMs);
        }
        [Route(URLs.DEVICE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            DevicesCreateEditVM devicesVMs = await database.Devices
                .Where(item => item.Id == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new DevicesCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    StatusId = item.StatusId,
                    DeviceType = item.DeviceType,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users
                }).FirstAsync();
            return Json(devicesVMs);
        }
        [Route(URLs.DEVICE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DevicesCreateEditVM devicesCreateEditVm)
        {
            DatabaseContext database = new();
            database.Devices.Add(new()
            {
                Name = devicesCreateEditVm.Name,
                StatusId = devicesCreateEditVm?.StatusId,
                DeviceType = devicesCreateEditVm?.DeviceType,
                Manufacturer = devicesCreateEditVm?.Manufacturer,
                Model = devicesCreateEditVm?.Model,
                SerialNumber = devicesCreateEditVm?.SerialNumber,
                InventoryNumber = devicesCreateEditVm?.InventoryNumber,
                Users = devicesCreateEditVm.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = devicesCreateEditVm?.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] DevicesCreateEditVM devicesCreateEditVm)
        {
            DatabaseContext database = new();
            Device device = await database.Devices.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (device == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            device.Name = devicesCreateEditVm.Name;
            device.StatusId = devicesCreateEditVm?.StatusId;
            device.DeviceType = devicesCreateEditVm?.DeviceType;
            device.Manufacturer = devicesCreateEditVm?.Manufacturer;
            device.Model = devicesCreateEditVm?.Model;
            device.SerialNumber = devicesCreateEditVm?.SerialNumber;
            device.InventoryNumber = devicesCreateEditVm?.InventoryNumber;
            device.Users = devicesCreateEditVm.Users;
            device.ModifiedAt = DateTime.Now;
            device.ModifiedBy = devicesCreateEditVm?.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.DEVICE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] DevicesCreateEditVM devicesCreateEditVm)
        {
            DatabaseContext database = new();
            Device device = await database.Devices.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (device == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            device.StatusId = 99;
            device.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
