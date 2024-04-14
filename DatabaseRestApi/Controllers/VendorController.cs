using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Runtime.Intrinsics.Arm;

namespace DatabaseRestApi.Controllers
{
    public class VendorController : Controller
    {
        [Route("/vendor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<VendorVM> vendorVMs = await database.Vendors
                .Where(item => item.IsActive == true)
                .Select(item => new VendorVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    AdministrativeNumber = item.AdministrativeNumber,
                    Phone = item.Phone,
                    Fax = item.Fax,
                    Website = item.Website,
                    Email = item.Email,
                    Address = item.Address,
                    City = item.City,
                    PostalCode = item.PostalCode,
                    Country = item.Country,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                }).ToListAsync();
            return Json(vendorVMs);
        }

        [Route("/vendor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorCreateEditVM vendorCreateEditVM)
        {
            DatabaseContext database = new();
            database.Vendors.Add(new()
            {
                Name = vendorCreateEditVM.Name,
                AdministrativeNumber = vendorCreateEditVM.AdministrativeNumber,
                Phone = vendorCreateEditVM.Phone,
                Fax = vendorCreateEditVM.Fax,
                Website = vendorCreateEditVM.Website,
                Email = vendorCreateEditVM.Email,
                Address = vendorCreateEditVM.Address,
                City = vendorCreateEditVM.City,
                PostalCode = vendorCreateEditVM.PostalCode,
                Country = vendorCreateEditVM.Country,
                Users = vendorCreateEditVM.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/vendor/{id}")]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] VendorCreateEditVM vendorCreateEditVM)
        {
            DatabaseContext database = new();
            Vendor? vendor = await database.Vendors.Where(item => item.Id == id  && item.IsActive == true).FirstOrDefaultAsync();
            if(vendor == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            vendor.Name = vendorCreateEditVM.Name;
            vendor.AdministrativeNumber = vendorCreateEditVM.AdministrativeNumber;
            vendor.Phone = vendorCreateEditVM.Phone;
            vendor.Fax = vendorCreateEditVM.Fax;
            vendor.Website = vendorCreateEditVM.Website;
            vendor.Email = vendorCreateEditVM.Email;
            vendor.Address = vendorCreateEditVM.Address;
            vendor.City = vendorCreateEditVM.City;
            vendor.PostalCode = vendorCreateEditVM.PostalCode;
            vendor.Country = vendorCreateEditVM.Country;
            vendor.Users = vendorCreateEditVM.Users;
            vendor.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/vendor/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] VendorCreateEditVM vendorCreateEditVM)
        {
            DatabaseContext database = new();
            Vendor? vendor = await database.Vendors.Where(item => item.Id == id && item.IsActive == true).FirstOrDefaultAsync();
            if (vendor == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            vendor.IsActive = false;
            vendor.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

    }
}
