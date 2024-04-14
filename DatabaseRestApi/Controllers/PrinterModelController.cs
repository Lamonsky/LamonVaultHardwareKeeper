using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PrinterModelController : Controller
    {
        [Route("/printermodel")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PrinterModelVM> printerModelVM = await database.PrinterModels
                .Select(item => new PrinterModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(printerModelVM);
        }
        [Route("/printermodel")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrinterModelCreateEditVM printerModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.PrinterModels.Add(new()
            {
                Name = printerModelCreateEditVM.Name,
                Comment = printerModelCreateEditVM.Comment,
                Status = printerModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/printermodel/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PrinterModelCreateEditVM printerModelCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterModel? printerModels = await database.PrinterModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerModels.Name = printerModelCreateEditVM.Name;
            printerModels.Comment = printerModelCreateEditVM.Comment;
            printerModels.Status = printerModelCreateEditVM.Status;
            printerModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/printermodel/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PrinterModelCreateEditVM printerModelCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterModel? printerModels = await database.PrinterModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerModels.Status = 99;
            printerModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
