using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
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
        [Route("/networkdevice")]
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
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(networkdeviceVM);
        }

        [Route("/networkdevice")]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevice/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevice/{id}")]
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
