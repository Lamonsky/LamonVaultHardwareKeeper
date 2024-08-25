using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class ComputerModelController : Controller
    {
        [Route(URLs.COMPUTERMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ComputerModelVM> computerModelVM = await database.ComputerModels
                .Where(item => item.Status != 99)
                .Select(item => new ComputerModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                }).ToListAsync();
            return Json(computerModelVM);
        }
        [Route(URLs.COMPUTERMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            ComputerModelVM computerModelVM = await database.ComputerModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new ComputerModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name
                }).FirstOrDefaultAsync();
            return Json(computerModelVM);
        }
        [Route(URLs.COMPUTERMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            ComputerModelCreateEditVM computerModelVM = await database.ComputerModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new ComputerModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status
                }).FirstAsync();
            return Json(computerModelVM);
        }
        [Route(URLs.COMPUTERMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComputerModelCreateEditVM computerModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.ComputerModels.Add(new()
            {
                Name = computerModelCreateEditVM.Name,
                Comment = computerModelCreateEditVM.Comment,
                Status = computerModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.COMPUTERMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ComputerModelCreateEditVM computerModelCreateEditVM)
        {
            DatabaseContext database = new();
            ComputerModel computerModels = await database.ComputerModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (computerModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            computerModels.Name = computerModelCreateEditVM.Name;
            computerModels.Comment = computerModelCreateEditVM.Comment;
            computerModels.Status = computerModelCreateEditVM.Status;
            computerModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.COMPUTERMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ComputerModelCreateEditVM computerModelCreateEditVM)
        {
            DatabaseContext database = new();
            ComputerModel computerModels = await database.ComputerModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (computerModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            computerModels.Status = 99;
            computerModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
