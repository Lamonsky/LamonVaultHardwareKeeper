using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class HardDriveController : Controller
    {
        [Route("/harddrive")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<HardDriveVM> hardDriveVMs = await database.HardDrives
                .Select(item => new HardDriveVM
                {
                    Id = item.Id,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    Capacity = item.Capacity.ToString(),
                    Server = item.ServerNavigation.Name + " " + item.ServerNavigation.InventoryNumber + " " + item.ServerNavigation.SerialNumber,
                    Status = item.StatusNavigation.Name,

                }).ToListAsync();
            return Json(hardDriveVMs);
        }
        [Route("/harddrive")]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/harddrive/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/harddrive/{id}")]
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
