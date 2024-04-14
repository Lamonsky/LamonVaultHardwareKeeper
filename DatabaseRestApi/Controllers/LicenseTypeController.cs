using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class LicenseTypeController : Controller
    {
        [Route("/licensetype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LicenseTypeVM> licenseTypeVM = await database.LicenseTypes
                .Select(item => new LicenseTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(licenseTypeVM);
        }
        [Route("/licensetype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseTypeCreateEditVM licenseTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.LicenseTypes.Add(new()
            {
                Name = licenseTypeCreateEditVM.Name,
                Comment = licenseTypeCreateEditVM.Comment,
                Status = licenseTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/licensetype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] LicenseTypeCreateEditVM licenseTypeCreateEditVM)
        {
            DatabaseContext database = new();
            LicenseType? licenseType = await database.LicenseTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (licenseType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            licenseType.Name = licenseTypeCreateEditVM.Name;
            licenseType.Comment = licenseTypeCreateEditVM.Comment;
            licenseType.Status = licenseTypeCreateEditVM.Status;
            licenseType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/licensetype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] LicenseTypeCreateEditVM licenseTypeCreateEditVM)
        {
            DatabaseContext database = new();
            LicenseType? licenseType = await database.LicenseTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (licenseType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            licenseType.Status = 99;
            licenseType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
