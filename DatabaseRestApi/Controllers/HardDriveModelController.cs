using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class HardDriveModelController : Controller
    {
        [Route(URLs.HARDDRIVEMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<HardDriveModelVM> hardDriveModelVM = await database.HardDriveModels
                .Where(item => item.Status != 99)                
                .Select(item => new HardDriveModelVM
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
            return Json(hardDriveModelVM);
        }
        [Route(URLs.HARDDRIVEMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            HardDriveModelVM hardDriveModelVM = await database.HardDriveModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new HardDriveModelVM
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
            return Json(hardDriveModelVM);
        }
        [Route(URLs.HARDDRIVEMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            HardDriveModelCreateEditVM hardDriveModelVM = await database.HardDriveModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new HardDriveModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(hardDriveModelVM);
        }
        [Route(URLs.HARDDRIVEMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HardDriveModelCreateEditVM hardDriveModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.HardDriveModels.Add(new()
            {
                Name = hardDriveModelCreateEditVM.Name,
                Comment = hardDriveModelCreateEditVM.Comment,
                Status = hardDriveModelCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = hardDriveModelCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.HARDDRIVEMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] HardDriveModelCreateEditVM hardDriveModelCreateEditVM)
        {
            DatabaseContext database = new();
            HardDriveModel? harddriveModels = await database.HardDriveModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (harddriveModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            harddriveModels.Name = hardDriveModelCreateEditVM.Name;
            harddriveModels.Comment = hardDriveModelCreateEditVM.Comment;
            harddriveModels.Status = hardDriveModelCreateEditVM.Status;
            harddriveModels.ModifiedAt = DateTime.Now;
            harddriveModels.ModifiedBy = hardDriveModelCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.HARDDRIVEMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] HardDriveModelCreateEditVM hardDriveModelCreateEditVM)
        {
            DatabaseContext database = new();
            HardDriveModel? harddriveModels = await database.HardDriveModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (harddriveModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            harddriveModels.Status = 99;
            harddriveModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
