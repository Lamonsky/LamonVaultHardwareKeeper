using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class HardDriveModelController : Controller
    {
        [Route("/harddrivemodel")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<HardDriveModelVM> hardDriveModelVM = await database.HardDriveModels
                .Select(item => new HardDriveModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(hardDriveModelVM);
        }
        [Route("/harddrivemodel")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HardDriveModelCreateEditVM hardDriveModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.HardDriveModels.Add(new()
            {
                Name = hardDriveModelCreateEditVM.Name,
                Comment = hardDriveModelCreateEditVM.Comment,
                Status = hardDriveModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/harddrivemodel/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/harddrivemodel/{id}")]
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
