using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class ServerController : Controller
    {
        [Authorize]
        [Route(URLs.SERVER)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ServerVM> serverVM = await database.Servers
                .Where(item => item.StatusId != 99)
                .Select(item => new ServerVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Processor = item.Processor,
                    Ram = item.Ram,
                    OperatingSystem = item.OperatingSystemNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(serverVM);
        }
        [Authorize]
        [Route(URLs.SERVER_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<ServerVM> serverVM = await database.Servers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new ServerVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Processor = item.Processor,
                    Ram = item.Ram,
                    OperatingSystem = item.OperatingSystemNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(serverVM);
        }
        [Authorize]
        [Route(URLs.SERVER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            ServerVM serverVM = await database.Servers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new ServerVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Processor = item.Processor,
                    Ram = item.Ram,
                    OperatingSystem = item.OperatingSystemNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(serverVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SERVER_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            ServerCreateEditVM serverVM = await database.Servers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new ServerCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    LocationId = item.LocationId,
                    StatusId = item.StatusId,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    Processor = item.Processor,
                    Ram = item.Ram,
                    OperatingSystem = item.OperatingSystem,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users

                }).FirstAsync();
            return Json(serverVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SERVER)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServerCreateEditVM serverCreateEditVM)
        {
            DatabaseContext database = new();
            database.Servers.Add(new()
            {
                Name = serverCreateEditVM.Name,
                LocationId = serverCreateEditVM.LocationId,
                StatusId = serverCreateEditVM.StatusId,
                Manufacturer = serverCreateEditVM.Manufacturer,
                Model = serverCreateEditVM.Model,
                Processor = serverCreateEditVM.Processor,
                Ram = serverCreateEditVM.Ram,
                OperatingSystem = serverCreateEditVM.OperatingSystem,
                SerialNumber = serverCreateEditVM.SerialNumber,
                InventoryNumber = serverCreateEditVM.InventoryNumber,
                Users = serverCreateEditVM.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = serverCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SERVER_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ServerCreateEditVM serverCreateEditVM)
        {
            DatabaseContext database = new();
            Server? server = await database.Servers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (server == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            server.Name = serverCreateEditVM.Name;
            server.LocationId = serverCreateEditVM.LocationId;
            server.StatusId = serverCreateEditVM.StatusId;
            server.Manufacturer = serverCreateEditVM.Manufacturer;
            server.Model = serverCreateEditVM.Model;
            server.Processor = serverCreateEditVM.Processor;
            server.Ram = serverCreateEditVM.Ram;
            server.OperatingSystem = serverCreateEditVM.OperatingSystem;
            server.SerialNumber = serverCreateEditVM.SerialNumber;
            server.InventoryNumber = serverCreateEditVM.InventoryNumber;
            server.Users = serverCreateEditVM.Users;
            server.ModifiedAt = DateTime.Now;
            server.ModifiedBy = serverCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SERVER_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ServerCreateEditVM serverCreateEditVM)
        {
            DatabaseContext database = new();
            Server? server = await database.Servers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (server == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            server.StatusId = 99;
            server.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
