using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Security.Policy;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class LicenseController : Controller
    {
        [Authorize]
        [Route(URLs.LICENSE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LicenseVM> licenseVM = await database.Licenses
                .Where(item => item.StatusId != 99)
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
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.InternalNumber,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(licenseVM);
        }
        [Authorize]
        [Route(URLs.LICENSE_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<LicenseVM> licenseVM = await database.Licenses
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
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
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.InternalNumber,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(licenseVM);
        }
        [Authorize]
        [Route(URLs.LICENSE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            LicenseVM licenseVM = await database.Licenses
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
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
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.InternalNumber,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(licenseVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.LICENSE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            LicenseCreateEditVM licenseVM = await database.Licenses
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new LicenseCreateEditVM
                {
                    Id = item.Id,
                    Software = item.Software,
                    Name = item.Name,
                    LicenseType = item.LicenseType,
                    Publisher = item.Publisher,
                    StatusId = item.StatusId,
                    LocationId = item.LocationId,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    ExpiryDate = item.ExpiryDate,
                    Users = item.Users

                }).FirstAsync();
            return Json(licenseVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.LICENSE)]
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
                CreatedAt = DateTime.Now,
                CreatedBy = licenseCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.LICENSE_ID)]
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
            license.ModifiedBy = licenseCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.LICENSE_ID)]
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
