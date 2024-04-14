using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class SimComponentController : Controller
    {
        [Route("/simcomponent")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SimComponentVM> simcomponentVMs = await database.SimComponents
                .Select(item => new SimComponentVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Type = item.TypeNavigation.Name,
                    Status = item.StatusNavigation.Name
                }).ToListAsync();
            return Json(simcomponentVMs);
        }
        [Route("/simcomponent")]
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
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/simcomponent/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/simcomponent/{id}")]
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
