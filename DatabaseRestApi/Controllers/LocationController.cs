using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class LocationController : Controller
    {
        [Route(URLs.LOCATION)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LocationVM> locationVM = await database.Locations
                .Where(item => item.Status != 99)             
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
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(locationVM);
        }
        [Route(URLs.LOCATION_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            LocationVM locationVM = await database.Locations
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
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
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(locationVM);
        }
        [Route(URLs.LOCATION_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            LocationCreateEditVM locationVM = await database.Locations
                .Where(item => item.Status != 99)             
                .Where(item => item.Id == id)
                .Select(item => new LocationCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    PostalCode = item.PostalCode,
                    City = item.City,
                    Country = item.Country,
                    BuildingNumber = item.BuildingNumber,
                    RoomNumber = item.RoomNumber,
                    Status = item.Status

                }).FirstAsync();
            return Json(locationVM);
        }
        [Route(URLs.LOCATION)]
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
                CreatedAt = DateTime.Now,
                CreatedBy = locationCreateEditVM.CreatedBy,
            }); ;

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.LOCATION_ID)]
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
            location.ModifiedBy = locationCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.LOCATION_ID)]
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
