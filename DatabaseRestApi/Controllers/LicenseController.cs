using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Security.Policy;

namespace DatabaseRestApi.Controllers
{
    public class LicenseController : Controller
    {
        [Route("/license")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LicenseVM> licenseVM = await database.Licenses
                .Select(item => new LicenseVM
                {
                    Id = item.Id,
                    Software = item.SoftwareNavigation.Name,
                    Name = item.Name,
                    LicenseType = item.LicenseTypeNavigation.Name,
                    Publisher = item.PublisherNavigation.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    ExpiryDate = item.ExpiryDate,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.InternalNumber

                }).ToListAsync();
            return Json(licenseVM);
        }
        [Route("/license")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseCreateEditVM licenseCreateEditVM)
        {
            DatabaseContext database = new();
            database.Licenses.Add(new()
            {

                Software = licenseCreateEditVM.Software,
                Name = licenseCreateEditVM.Name,
                LicenseType = licenseCreateEditVM.LicenseType,
                Publisher = licenseCreateEditVM.Publisher,
                StatusId = licenseCreateEditVM.StatusId,
                LocationId = licenseCreateEditVM.LocationId,
                SerialNumber = licenseCreateEditVM.SerialNumber,
                InventoryNumber = licenseCreateEditVM.InventoryNumber,
                ExpiryDate = licenseCreateEditVM.ExpiryDate,
                Users = licenseCreateEditVM.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/license/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] LicenseCreateEditVM licenseCreateEditVM)
        {
            DatabaseContext database = new();
            License license = await database.Licenses.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (license == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            license.Software = licenseCreateEditVM.Software;
            license.Name = licenseCreateEditVM.Name;
            license.LicenseType = licenseCreateEditVM.LicenseType;
            license.Publisher = licenseCreateEditVM.Publisher;
            license.StatusId = licenseCreateEditVM.StatusId;
            license.LocationId = licenseCreateEditVM.LocationId;
            license.SerialNumber = licenseCreateEditVM.SerialNumber;
            license.InventoryNumber = licenseCreateEditVM.InventoryNumber;
            license.ExpiryDate = licenseCreateEditVM.ExpiryDate;
            license.Users = licenseCreateEditVM.Users;
            license.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/license/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] LicenseCreateEditVM licenseCreateEditVM)
        {
            DatabaseContext database = new();
            License license = await database.Licenses.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (license == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            license.StatusId = 99;
            license.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
