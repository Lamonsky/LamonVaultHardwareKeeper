using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;

namespace DatabaseRestApi.Controllers
{
    public class SoftwareController : Controller
    {
        [Route(URLs.SOFTWARE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SoftwaresVM> softwareVM = await database.Softwares
                .Where(item => item.Status != 99)
                .Select(item => new SoftwaresVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Publisher = item.PublisherNavigation.Name,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    Status = item.StatusNavigation.Name,
                }).ToListAsync();
            return Json(softwareVM);
        }
        [Route(URLs.SOFTWARE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            SoftwaresVM softwareVM = await database.Softwares
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new SoftwaresVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Publisher = item.PublisherNavigation.Name,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    Status = item.StatusNavigation.Name,
                }).FirstAsync();
            return Json(softwareVM);
        }
        [Route(URLs.SOFTWARE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            SoftwareCreateEditVM softwareVM = await database.Softwares
                .Where(item => item.Status != 99)
                .Select(item => new SoftwareCreateEditVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    LocationId = item.LocationId,
                    Publisher = item.Publisher,
                    Users = item.Users,
                    Status = item.Status,
                }).FirstAsync();
            return Json(softwareVM);
        }

        [Route(URLs.SOFTWARE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SoftwareCreateEditVM softwareCreateEditVM)
        {
            DatabaseContext database = new();
            database.Softwares.Add(new()
            {
                Name = softwareCreateEditVM.Name,
                LocationId = softwareCreateEditVM.LocationId,
                Publisher = softwareCreateEditVM.Publisher,
                Users = softwareCreateEditVM.Users,
                Status = softwareCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SOFTWARE_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] SoftwareCreateEditVM softwareCreateEditVM)
        {
            DatabaseContext database = new();
            Software? software = await database.Softwares.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (software == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            software.Name = softwareCreateEditVM.Name;
            software.LocationId = softwareCreateEditVM.LocationId;
            software.Publisher = softwareCreateEditVM.Publisher;
            software.Users = softwareCreateEditVM.Users;
            software.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SOFTWARE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] SoftwareCreateEditVM softwareCreateEditVM)
        {
            DatabaseContext database = new();
            Software? software = await database.Softwares.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (software == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            software.Status = 99;
            software.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
