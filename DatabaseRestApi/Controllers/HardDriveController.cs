using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class HardDriveController : Controller
    {
        [Authorize]
        [Route(URLs.HARDDRIVE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<HardDriveVM> hardDriveVMs = await database.HardDrives
                .Where(item => item.Status != 99)
                .Select(item => new HardDriveVM
                {
                    Id = item.Id,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Capacity = item.Capacity.ToString(),
                    Server = item.ServerNavigation.Name + " " + item.ServerNavigation.InventoryNumber + " " + item.ServerNavigation.SerialNumber,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(hardDriveVMs);
        }
        [Authorize]
        [Route(URLs.HARDDRIVE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            HardDriveVM hardDriveVMs = await database.HardDrives
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new HardDriveVM
                {
                    Id = item.Id,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Capacity = item.Capacity.ToString(),
                    Server = item.ServerNavigation.Name + " " + item.ServerNavigation.InventoryNumber + " " + item.ServerNavigation.SerialNumber,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(hardDriveVMs);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.HARDDRIVE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            HardDriveCreateEditVM hardDriveVMs = await database.HardDrives
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new HardDriveCreateEditVM
                {
                    Id = item.Id,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    Capacity = item.Capacity,
                    Server = item.Server,
                    Status = item.Status,

                }).FirstAsync();
            return Json(hardDriveVMs);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.HARDDRIVE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HardDriveCreateEditVM hardDriveCreateEditVM)
        {
            DatabaseContext database = new();
            database.HardDrives.Add(new()
            {
                Manufacturer = hardDriveCreateEditVM.Manufacturer,
                Model = hardDriveCreateEditVM.Model,
                Capacity = hardDriveCreateEditVM.Capacity,
                Server = hardDriveCreateEditVM.Server,
                Status = hardDriveCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = hardDriveCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.HARDDRIVE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] HardDriveCreateEditVM hardDriveCreateEditVM)
        {
            DatabaseContext database = new();
            HardDrife? harddrive = await database.HardDrives.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (harddrive == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            harddrive.Manufacturer = hardDriveCreateEditVM.Manufacturer;
            harddrive.Model = hardDriveCreateEditVM.Model;
            harddrive.Capacity = hardDriveCreateEditVM.Capacity;
            harddrive.Server = hardDriveCreateEditVM.Server;
            harddrive.Status = hardDriveCreateEditVM.Status;
            harddrive.ModifiedAt = DateTime.Now;
            harddrive.ModifiedBy = hardDriveCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.HARDDRIVE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] HardDriveCreateEditVM hardDriveCreateEditVM)
        {
            DatabaseContext database = new();
            HardDrife? harddrive = await database.HardDrives.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (harddrive == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            harddrive.Status = 99;
            harddrive.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
