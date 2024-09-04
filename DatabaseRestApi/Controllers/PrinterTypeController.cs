using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PrinterTypeController : Controller
    {
        [Route(URLs.PRINTERTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PrinterTypeVM> printerTypeVM = await database.PrinterTypes
                .Where(item => item.Status != 99)
                .Select(item => new PrinterTypeVM
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
            return Json(printerTypeVM);
        }
        [Route(URLs.PRINTERTYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PrinterTypeVM printerTypeVM = await database.PrinterTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PrinterTypeVM
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
            return Json(printerTypeVM);
        }
        [Route(URLs.PRINTERTYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PrinterTypeCreateEditVM printerTypeVM = await database.PrinterTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PrinterTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status,


                }).FirstAsync();
            return Json(printerTypeVM);
        }
        [Route(URLs.PRINTERTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.PrinterTypes.Add(new()
            {
                Name = printerTypeCreateEditVM.Name,
                Comment = printerTypeCreateEditVM.Comment,
                Status = printerTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = printerTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERTYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterType? printerType = await database.PrinterTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerType.Name = printerTypeCreateEditVM.Name;
            printerType.Comment = printerTypeCreateEditVM.Comment;
            printerType.Status = printerTypeCreateEditVM.Status;
            printerType.ModifiedAt = DateTime.Now;
            printerType.ModifiedBy = printerTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERTYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterType? printerType = await database.PrinterTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerType.Status = 99;
            printerType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
