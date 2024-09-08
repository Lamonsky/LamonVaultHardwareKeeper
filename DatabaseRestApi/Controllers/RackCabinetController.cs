using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class RackCabinetController : Controller
    {
        [Route(URLs.RACKCABINET)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<RackCabinetVM> rackcabinetVM = await database.RackCabinets
                .Where(item => item.StatusId != 99)
                .Select(item => new RackCabinetVM()
                {
                    Id = item.Id,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    CabinetType = item.CabinetTypeNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    Height = item.Height.ToString(),
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(rackcabinetVM);
        }
        [Route(URLs.RACKCABINET_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<RackCabinetVM> rackcabinetVM = await database.RackCabinets
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new RackCabinetVM()
                {
                    Id = item.Id,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    CabinetType = item.CabinetTypeNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    Height = item.Height.ToString(),
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(rackcabinetVM);
        }
        [Route(URLs.RACKCABINET_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            RackCabinetVM rackcabinetVM = await database.RackCabinets
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new RackCabinetVM()
                {
                    Id = item.Id,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    CabinetType = item.CabinetTypeNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    Height = item.Height.ToString(),
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(rackcabinetVM);
        }
        [Route(URLs.RACKCABINET_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            RackCabinetCreateEditVM rackcabinetVM = await database.RackCabinets
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new RackCabinetCreateEditVM()
                {
                    Id = item.Id,
                    LocationId = item.LocationId,
                    StatusId = item.StatusId,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    CabinetType = item.CabinetType,
                    SerialNumber = item.SerialNumber,
                    Height = item.Height,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users
                }).FirstAsync();
            return Json(rackcabinetVM);
        }

        [Route(URLs.RACKCABINET)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
        {
            DatabaseContext database = new();
            database.RackCabinets.Add(new()
            {
                LocationId = RackCabinetCreateEditVM.LocationId,
                StatusId = RackCabinetCreateEditVM.StatusId,
                Manufacturer = RackCabinetCreateEditVM.Manufacturer,
                Model = RackCabinetCreateEditVM.Model,
                Height = RackCabinetCreateEditVM.Height,
                CabinetType = RackCabinetCreateEditVM.CabinetType,
                SerialNumber=RackCabinetCreateEditVM.SerialNumber,
                InventoryNumber = RackCabinetCreateEditVM.InventoryNumber,
                Users = RackCabinetCreateEditVM.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = RackCabinetCreateEditVM.CreatedBy
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINET_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinet rackcabinet = await database.RackCabinets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (rackcabinet == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinet.LocationId = RackCabinetCreateEditVM.LocationId;
            rackcabinet.StatusId = RackCabinetCreateEditVM.StatusId;
            rackcabinet.Manufacturer = RackCabinetCreateEditVM.Manufacturer;
            rackcabinet.Model = RackCabinetCreateEditVM.Model;
            rackcabinet.Height = RackCabinetCreateEditVM.Height;
            rackcabinet.CabinetType = RackCabinetCreateEditVM.CabinetType;
            rackcabinet.SerialNumber=RackCabinetCreateEditVM.SerialNumber;
            rackcabinet.InventoryNumber = RackCabinetCreateEditVM.InventoryNumber;
            rackcabinet.Users = RackCabinetCreateEditVM.Users;
            rackcabinet.ModifiedAt = DateTime.Now;
            rackcabinet.ModifiedBy = RackCabinetCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINET_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinet rackcabinet = await database.RackCabinets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (rackcabinet == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinet.StatusId = 99;
            rackcabinet.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
