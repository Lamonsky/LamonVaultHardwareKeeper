using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class SimComponentTypeController : Controller
    {
        [Authorize]
        [Route(URLs.SIMCOMPONENTTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SimComponentTypeVM> simcomponentTypeVM = await database.SimComponentTypes
                .Where(item => item.Status != 99)
                .Select(item => new SimComponentTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(simcomponentTypeVM);
        }
        [Authorize]
        [Route(URLs.SIMCOMPONENTTYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            SimComponentTypeVM simcomponentTypeVM = await database.SimComponentTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimComponentTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(simcomponentTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SIMCOMPONENTTYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            SimComponentTypeCreateEditVM simcomponentTypeVM = await database.SimComponentTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimComponentTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status,
                    
                }).FirstAsync();
            return Json(simcomponentTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SIMCOMPONENTTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimComponentTypeCreateEditVM simcomponentTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.SimComponentTypes.Add(new()
            {
                Name = simcomponentTypeCreateEditVM.Name,
                Comment = simcomponentTypeCreateEditVM.Comment,
                Status = simcomponentTypeCreateEditVM.Status,
                CreatedBy = simcomponentTypeCreateEditVM.CreatedBy,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SIMCOMPONENTTYPE_ID)]
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
            simcomponentType.ModifiedBy = simcomponentTypeCreateEditVM.ModifiedBy;
            simcomponentType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.SIMCOMPONENTTYPE_ID)]
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
