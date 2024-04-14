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
        [Route("/devices")]
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
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(devicesVMs);
        }
        [Route("/devices")]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/devices/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/devices/{id}")]
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
