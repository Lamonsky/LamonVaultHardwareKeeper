using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class ManufacturerController : Controller
    {
        [Route(URLs.MANUFACTURER)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ManufacturerVM> manufacturerVM = await database.Manufacturers
                .Where(item => item.Status != 99)
                .Select(item => new ManufacturerVM
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
            return Json(manufacturerVM);
        }
        [Route(URLs.MANUFACTURER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            ManufacturerVM manufacturerVM = await database.Manufacturers
                .Where(item => item.Id == id)
                .Where(item => item.Status != 99)
                .Select(item => new ManufacturerVM
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
            return Json(manufacturerVM);
        }
        [Route(URLs.MANUFACTURER_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            ManufacturerCreateEditVM manufacturerVM = await database.Manufacturers
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new ManufacturerCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(manufacturerVM);
        }
        [Route(URLs.MANUFACTURER)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManufacturerCreateEditVM manufacturerCreateEditVM)
        {
            DatabaseContext database = new();
            database.Manufacturers.Add(new()
            {
                Name = manufacturerCreateEditVM.Name,
                Comment = manufacturerCreateEditVM.Comment,
                Status = manufacturerCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = manufacturerCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MANUFACTURER_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ManufacturerCreateEditVM manufacturerCreateEditVM)
        {
            DatabaseContext database = new();
            Manufacturer? manufacturer = await database.Manufacturers.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (manufacturer == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            manufacturer.Name = manufacturerCreateEditVM.Name;
            manufacturer.Comment = manufacturerCreateEditVM.Comment;
            manufacturer.Status = manufacturerCreateEditVM.Status;
            manufacturer.ModifiedAt = DateTime.Now;
            manufacturer.ModifiedBy = manufacturerCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MANUFACTURER_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ManufacturerCreateEditVM manufacturerCreateEditVM)
        {
            DatabaseContext database = new();
            Manufacturer? manufacturer = await database.Manufacturers.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (manufacturer == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            manufacturer.Status = 99;
            manufacturer.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
