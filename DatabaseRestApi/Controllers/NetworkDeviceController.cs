using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;

namespace DatabaseRestApi.Controllers
{   
    public class NetworkDeviceController : Controller
    {
        [Authorize]
        [Route(URLs.NETWORKDEVICE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<NetworkDeviceVM> networkdeviceVM = await database.NetworkDevices
                .Where(item => item.StatusId != 99)
                .Select(item => new NetworkDeviceVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Rack = item.RackNavigation.Manufacturer + " " + item.RackNavigation.ModelNavigation.Name + " " + item.RackNavigation.Location.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(networkdeviceVM);
        }
        [Authorize]
        [Route(URLs.NETWORKDEVICE_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<NetworkDeviceVM> networkdeviceVM = await database.NetworkDevices
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new NetworkDeviceVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Rack = item.RackNavigation.Manufacturer + " " + item.RackNavigation.ModelNavigation.Name + " " + item.RackNavigation.Location.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(networkdeviceVM);
        }
        [Authorize]
        [Route(URLs.NETWORKDEVICE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            NetworkDeviceVM networkdeviceVM = await database.NetworkDevices
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new NetworkDeviceVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    DeviceType = item.DeviceTypeNavigation.Name,
                    Rack = item.RackNavigation.Manufacturer + " " + item.RackNavigation.ModelNavigation.Name + " " + item.RackNavigation.Location.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(networkdeviceVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            NetworkDeviceCreateEditVM networkdeviceVM = await database.NetworkDevices
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new NetworkDeviceCreateEditVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    LocationId = item.LocationId,
                    StatusId = item.StatusId,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    DeviceType = item.DeviceType,
                    Rack = item.Rack,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users
                }).FirstAsync();
            return Json(networkdeviceVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NetworkDeviceCreateEditVM NetworkDeviceCreateEditVM)
        {
            DatabaseContext database = new();
            database.NetworkDevices.Add(new()
            {
                Name = NetworkDeviceCreateEditVM.Name,
                LocationId = NetworkDeviceCreateEditVM.LocationId,
                StatusId = NetworkDeviceCreateEditVM.StatusId,
                Manufacturer = NetworkDeviceCreateEditVM.Manufacturer,
                Model = NetworkDeviceCreateEditVM.Model,
                DeviceType = NetworkDeviceCreateEditVM.DeviceType,
                Rack = NetworkDeviceCreateEditVM.Rack,
                SerialNumber=NetworkDeviceCreateEditVM.SerialNumber,
                InventoryNumber = NetworkDeviceCreateEditVM.InventoryNumber,
                Users = NetworkDeviceCreateEditVM.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = NetworkDeviceCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICE_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] NetworkDeviceCreateEditVM NetworkDeviceCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDevice? networkdevice = await database.NetworkDevices.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (networkdevice == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            networkdevice.Name = NetworkDeviceCreateEditVM.Name;
            networkdevice.LocationId = NetworkDeviceCreateEditVM.LocationId;
            networkdevice.StatusId = NetworkDeviceCreateEditVM.StatusId;
            networkdevice.Manufacturer = NetworkDeviceCreateEditVM.Manufacturer;
            networkdevice.Model = NetworkDeviceCreateEditVM.Model;
            networkdevice.DeviceType = NetworkDeviceCreateEditVM.DeviceType;
            networkdevice.Rack = NetworkDeviceCreateEditVM.Rack;
            networkdevice.SerialNumber=NetworkDeviceCreateEditVM.SerialNumber;
            networkdevice.InventoryNumber = NetworkDeviceCreateEditVM.InventoryNumber;
            networkdevice.Users = NetworkDeviceCreateEditVM.Users;
            networkdevice.ModifiedAt = DateTime.Now;
            networkdevice.ModifiedBy = NetworkDeviceCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] NetworkDeviceCreateEditVM NetworkDeviceCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDevice? networkdevice = await database.NetworkDevices.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (networkdevice == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            networkdevice.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
