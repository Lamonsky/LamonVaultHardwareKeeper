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

namespace DatabaseRestApi.Controllers
{
    public class ComputerController : Controller
    {
        [Route(URLs.COMPUTERS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ComputersVM> computerVMs = await database.Computers
                .Where(item => item.StatusId != 99)
                .Select(item => new ComputersVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ManufacturerName = item.ManufacturerNavigation.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    ComputerType = item.ComputerTypeNavigation.Name,
                    ComputerModel = item.ModelNavigation.Name,
                    Processor = item.Processor,
                    Ram = item.Ram,
                    Disk = item.Disk,
                    GraphicsCard = item.GraphicsCard,
                    OperatingSystem = item.OperatingSystemNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(computerVMs);
        }

        [Authorize]
        [Route(URLs.COMPUTERS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComputersCreateEditVM computersCreateEditVM)
        {
            DatabaseContext database = new();
            database.Computers.Add(new()
            {
                Name = computersCreateEditVM.Name,
                LocationId = computersCreateEditVM.LocationId,
                StatusId = computersCreateEditVM.StatusId,
                ComputerType = computersCreateEditVM.ComputerTypeId,
                Manufacturer = computersCreateEditVM.ManufacturerId,
                Model = computersCreateEditVM.ComputerModelId,
                Processor = computersCreateEditVM.Processor,
                Ram = computersCreateEditVM.Ram,
                Disk = computersCreateEditVM.Disk,
                GraphicsCard = computersCreateEditVM.GraphicsCard,
                OperatingSystem = computersCreateEditVM.OperatingSystemId,
                SerialNumber = computersCreateEditVM.SerialNumber,
                InventoryNumber = computersCreateEditVM.InventoryNumber,
                Users = computersCreateEditVM.UserId,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.COMPUTERS_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] ComputersCreateEditVM computersCreateEditVM)
        {
            DatabaseContext database = new();
            Computer? computers = await database.Computers.Where(item => item.Id == id  && item.StatusId != 99).FirstOrDefaultAsync();
            if(computers == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            computers.Name = computersCreateEditVM.Name;
            computers.LocationId = computersCreateEditVM.LocationId;
            computers.StatusId = computersCreateEditVM.StatusId;
            computers.ComputerType = computersCreateEditVM.ComputerTypeId;
            computers.Manufacturer = computersCreateEditVM.ManufacturerId;
            computers.Model = computersCreateEditVM.ComputerModelId;
            computers.Processor = computersCreateEditVM.Processor;
            computers.Ram = computersCreateEditVM.Ram;
            computers.Disk = computersCreateEditVM.Disk;
            computers.GraphicsCard = computersCreateEditVM.GraphicsCard;
            computers.OperatingSystem = computersCreateEditVM.OperatingSystemId;
            computers.SerialNumber = computersCreateEditVM.SerialNumber;
            computers.InventoryNumber = computersCreateEditVM.InventoryNumber;
            computers.Users = computersCreateEditVM.UserId;
            computers.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.COMPUTERS_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ComputersCreateEditVM computersCreateEditVM)
        {
            DatabaseContext database = new();
            Computer? computers = await database.Computers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (computers == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            computers.StatusId = 99;
            computers.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

    }
}
