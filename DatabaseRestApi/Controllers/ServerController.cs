using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace DatabaseRestApi.Controllers
{
    public class ServerController : Controller
    {
        [Route("/server")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ServerVM> serverVM = await database.Servers
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
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName

                }).ToListAsync();
            return Json(serverVM);
        }
        [Route("/server")]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/server/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/server/{id}")]
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
