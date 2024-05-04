using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class RackCabinetTypeController : Controller
    {
        [Route(URLs.RACKCABINETTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<RackCabinetTypeVM> rackcabinetTypeVM = await database.RackCabinetTypes
                .Select(item => new RackCabinetTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                    
                }).ToListAsync();
            return Json(rackcabinetTypeVM);
        }
        [Route(URLs.RACKCABINETTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RackCabinetTypeCreateEditVM rackcabinetTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.RackCabinetTypes.Add(new()
            {
                Name = rackcabinetTypeCreateEditVM.Name,
                Comment = rackcabinetTypeCreateEditVM.Comment,
                Status = rackcabinetTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINETTYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] RackCabinetTypeCreateEditVM rackcabinetTypeCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinetType? rackcabinetType = await database.RackCabinetTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (rackcabinetType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinetType.Name = rackcabinetTypeCreateEditVM.Name;
            rackcabinetType.Comment = rackcabinetTypeCreateEditVM.Comment;
            rackcabinetType.Status = rackcabinetTypeCreateEditVM.Status;
            rackcabinetType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINETTYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] RackCabinetTypeCreateEditVM rackcabinetTypeCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinetType? rackcabinetType = await database.RackCabinetTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (rackcabinetType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinetType.Status = 99;
            rackcabinetType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
