using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class SimComponentController : Controller
    {
        [Route(URLs.SIMCOMPONENT)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SimComponentVM> simcomponentVMs = await database.SimComponents
                .Where(item => item.Status != 99)
                .Select(item => new SimComponentVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Type = item.TypeNavigation.Name,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(simcomponentVMs);
        }
        [Route(URLs.SIMCOMPONENT_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            SimComponentVM simcomponentVMs = await database.SimComponents
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimComponentVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Type = item.TypeNavigation.Name,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(simcomponentVMs);
        }
        [Route(URLs.SIMCOMPONENT_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            SimComponentCreateEditVM simcomponentVMs = await database.SimComponents
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimComponentCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Manufacturer = (int)item.Manufacturer,
                    Type = (int)item.Type,
                    Status = (int)item.Status
                }).FirstAsync();
            return Json(simcomponentVMs);
        }
        [Route(URLs.SIMCOMPONENT)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimComponentCreateEditVM simComponentCreateEditVM)
        {
            DatabaseContext database = new();
            database.SimComponents.Add(new()
            {
                Name = simComponentCreateEditVM.Name,
                Manufacturer = simComponentCreateEditVM.Manufacturer,
                Type = simComponentCreateEditVM.Type,
                Status = simComponentCreateEditVM.Status,
                CreatedBy = simComponentCreateEditVM.CreatedBy,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SIMCOMPONENT_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] SimComponentCreateEditVM simComponentCreateEditVM)
        {
            DatabaseContext database = new();
            SimComponent simComponent = await database.SimComponents.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (simComponent == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simComponent.Name = simComponentCreateEditVM.Name;
            simComponent.Manufacturer = simComponentCreateEditVM.Manufacturer;
            simComponent.Type = simComponentCreateEditVM.Type;
            simComponent.Status = simComponentCreateEditVM.Status;
            simComponent.ModifiedAt = DateTime.Now;
            simComponent.ModifiedBy = simComponentCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SIMCOMPONENT_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] SimComponentCreateEditVM simComponentCreateEditVM)
        {
            DatabaseContext database = new();
            SimComponent simComponent = await database.SimComponents.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (simComponent == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simComponent.Status = 99;
            simComponent.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
