using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PrinterModelController : Controller
    {
        [Route(URLs.PRINTERMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PrinterModelVM> printerModelVM = await database.PrinterModels
                .Where(item => item.Status != 99)
                .Select(item => new PrinterModelVM
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
            return Json(printerModelVM);
        }
        [Route(URLs.PRINTERMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PrinterModelVM printerModelVM = await database.PrinterModels
                .Where(item => item.Status != 99)
                .Select(item => new PrinterModelVM
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
            return Json(printerModelVM);
        }
        [Route(URLs.PRINTERMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PrinterModelCreateEditVM printerModelVM = await database.PrinterModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PrinterModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status
                }).FirstAsync();
            return Json(printerModelVM);
        }
        [Route(URLs.PRINTERMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrinterModelCreateEditVM printerModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.PrinterModels.Add(new()
            {
                Name = printerModelCreateEditVM.Name,
                Comment = printerModelCreateEditVM.Comment,
                Status = printerModelCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = printerModelCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERMODEL_ID)]
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
            printerModels.ModifiedBy = printerModelCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERMODEL_ID)]
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
