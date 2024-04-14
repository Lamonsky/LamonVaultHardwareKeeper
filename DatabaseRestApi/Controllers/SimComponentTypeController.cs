using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class SimComponentTypeController : Controller
    {
        [Route("/simcomponenttype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SimComponentTypeVM> simcomponentTypeVM = await database.SimComponentTypes
                .Select(item => new SimComponentTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    
                }).ToListAsync();
            return Json(simcomponentTypeVM);
        }
        [Route("/simcomponenttype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimComponentTypeCreateEditVM simcomponentTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.SimComponentTypes.Add(new()
            {
                Name = simcomponentTypeCreateEditVM.Name,
                Comment = simcomponentTypeCreateEditVM.Comment,
                Status = simcomponentTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/simcomponenttype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] SimComponentTypeCreateEditVM simcomponentTypeCreateEditVM)
        {
            DatabaseContext database = new();
            SimComponentType? simcomponentType = await database.SimComponentTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (simcomponentType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simcomponentType.Name = simcomponentTypeCreateEditVM.Name;
            simcomponentType.Comment = simcomponentTypeCreateEditVM.Comment;
            simcomponentType.Status = simcomponentTypeCreateEditVM.Status;
            simcomponentType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/simcomponenttype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] SimComponentTypeCreateEditVM simcomponentTypeCreateEditVM)
        {
            DatabaseContext database = new();
            SimComponentType? simcomponentType = await database.SimComponentTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (simcomponentType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simcomponentType.Status = 99;
            simcomponentType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
