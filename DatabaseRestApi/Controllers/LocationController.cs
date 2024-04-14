using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace DatabaseRestApi.Controllers
{
    public class LocationController : Controller
    {
        [Route("/location")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LocationVM> locationVM = await database.Locations
                .Select(item => new LocationVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    PostalCode = item.PostalCode,
                    City = item.City,
                    Country = item.Country,
                    BuildingNumber = item.BuildingNumber,
                    RoomNumber = item.RoomNumber,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(locationVM);
        }
        [Route("/location")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationCreateEditVM locationCreateEditVM)
        {
            DatabaseContext database = new();
            database.Locations.Add(new()
            {
                Name = locationCreateEditVM.Name,
                Address = locationCreateEditVM.Address,
                PostalCode = locationCreateEditVM.PostalCode,
                City = locationCreateEditVM.City,
                Country = locationCreateEditVM.Country,
                BuildingNumber = locationCreateEditVM.BuildingNumber,
                RoomNumber = locationCreateEditVM.RoomNumber,
                Status = locationCreateEditVM.Status,
                CreatedAt = DateTime.Now
            }); ;

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/location/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] LocationCreateEditVM locationCreateEditVM)
        {
            DatabaseContext database = new();
            Location location = await database.Locations.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (location == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            location.Name = locationCreateEditVM.Name;
            location.Address = locationCreateEditVM.Address;
            location.PostalCode = locationCreateEditVM.PostalCode;
            location.City = locationCreateEditVM.City;
            location.Country = locationCreateEditVM.Country;
            location.BuildingNumber = locationCreateEditVM.BuildingNumber;
            location.RoomNumber = locationCreateEditVM.RoomNumber;
            location.Status = locationCreateEditVM.Status;
            location.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/location/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] LocationCreateEditVM locationCreateEditVM)
        {
            DatabaseContext database = new();
            Location location = await database.Locations.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (location == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            location.Status = 99;
            location.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
