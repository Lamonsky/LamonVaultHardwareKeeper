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
    public class PositionController : Controller
    {
        [Authorize]
        [Route(URLs.POSITION)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PositionVM> positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Select(item => new PositionVM
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
            return Json(positionVM);
        }
        [Authorize]
        [Route(URLs.POSITION_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PositionVM positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PositionVM
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
            return Json(positionVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.POSITION_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PositionCreateEditVM positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PositionCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(positionVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.POSITION)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            database.Positions.Add(new()
            {
                Name = positionCreateEditVM.Name,
                Comment = positionCreateEditVM.Comment,
                Status = positionCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = positionCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.POSITION_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            Position? position = await database.Positions.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (position == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            position.Name = positionCreateEditVM.Name;
            position.Comment = positionCreateEditVM.Comment;
            position.Status = positionCreateEditVM.Status;
            position.ModifiedAt = DateTime.Now;
            position.ModifiedBy = positionCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.POSITION_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            Position? position = await database.Positions.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (position == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            position.Status = 99;
            position.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
