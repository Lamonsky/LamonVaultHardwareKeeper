using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class RackCabinetModelController : Controller
    {
        [Route(URLs.RACKCABINETMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<RackCabinetModelVM> rackcabinetModelVM = await database.RackCabinetModels
                .Where(item => item.Status != 99)
                .Select(item => new RackCabinetModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                    
                }).ToListAsync();
            return Json(rackcabinetModelVM);
        }
        [Route(URLs.RACKCABINETMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            RackCabinetModelVM rackcabinetModelVM = await database.RackCabinetModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new RackCabinetModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                    
                }).FirstAsync();
            return Json(rackcabinetModelVM);
        }
        [Route(URLs.RACKCABINETMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            RackCabinetModelCreateEditVM rackcabinetModelVM = await database.RackCabinetModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new RackCabinetModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status
                    
                }).FirstAsync();
            return Json(rackcabinetModelVM);
        }
        [Route(URLs.RACKCABINETMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RackCabinetModelCreateEditVM rackcabinetModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.RackCabinetModels.Add(new()
            {
                Name = rackcabinetModelCreateEditVM.Name,
                Comment = rackcabinetModelCreateEditVM.Comment,
                Status = rackcabinetModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINETMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] RackCabinetModelCreateEditVM rackcabinetModelCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinetModel? rackcabinetModels = await database.RackCabinetModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (rackcabinetModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinetModels.Name = rackcabinetModelCreateEditVM.Name;
            rackcabinetModels.Comment = rackcabinetModelCreateEditVM.Comment;
            rackcabinetModels.Status = rackcabinetModelCreateEditVM.Status;
            rackcabinetModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.RACKCABINETMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] RackCabinetModelCreateEditVM rackcabinetModelCreateEditVM)
        {
            DatabaseContext database = new();
            RackCabinetModel? rackcabinetModels = await database.RackCabinetModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (rackcabinetModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            rackcabinetModels.Status = 99;
            rackcabinetModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
