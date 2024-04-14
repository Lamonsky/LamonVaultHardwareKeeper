using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class RackCabinetController : Controller
    {
        public class NetworkDeviceController : Controller
        {
            [Route("/rackcabinet")]
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                DatabaseContext database = new();
                List<RackCabinetVM> rackcabinetVM = await database.RackCabinets
                    .Where(item => item.StatusId != 99)
                    .Select(item => new RackCabinetVM()
                    {
                        Id = item.Id,
                        Location = item.Location.Name,
                        Status = item.Status.Name,
                        Manufacturer = item.ManufacturerNavigation.Name,
                        Model = item.ModelNavigation.Name,
                        CabinetType = item.CabinetTypeNavigation.Name,
                        SerialNumber = item.SerialNumber,
                        Height = item.Height.ToString(),
                        InventoryNumber = item.InventoryNumber,
                        Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                    }).ToListAsync();
                return Json(rackcabinetVM);
            }

            [Route("/rackcabinet")]
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
            {
                DatabaseContext database = new();
                database.RackCabinets.Add(new()
                {
                    LocationId = RackCabinetCreateEditVM.LocationId,
                    StatusId = RackCabinetCreateEditVM.StatusId,
                    Manufacturer = RackCabinetCreateEditVM.Manufacturer,
                    Model = RackCabinetCreateEditVM.Model,
                    Height = RackCabinetCreateEditVM.Height,
                    CabinetType = RackCabinetCreateEditVM.CabinetType,
                    SerialNumber=RackCabinetCreateEditVM.SerialNumber,
                    InventoryNumber = RackCabinetCreateEditVM.InventoryNumber,
                    Users = RackCabinetCreateEditVM.Users,
                    CreatedAt = DateTime.Now
                });

                await database.SaveChangesAsync();
                return Ok();
            }

            [Route("/rackcabinet/{id}")]
            [HttpPut]
            public async Task<IActionResult> Edit(int id, [FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
            {
                DatabaseContext database = new();
                RackCabinet rackcabinet = await database.RackCabinets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
                if (rackcabinet == null)
                {
                    return BadRequest("Nie ma komputera o podanym id {id}");
                }
                rackcabinet.LocationId = RackCabinetCreateEditVM.LocationId;
                rackcabinet.StatusId = RackCabinetCreateEditVM.StatusId;
                rackcabinet.Manufacturer = RackCabinetCreateEditVM.Manufacturer;
                rackcabinet.Model = RackCabinetCreateEditVM.Model;
                rackcabinet.Height = RackCabinetCreateEditVM.Height;
                rackcabinet.CabinetType = RackCabinetCreateEditVM.CabinetType;
                rackcabinet.SerialNumber=RackCabinetCreateEditVM.SerialNumber;
                rackcabinet.InventoryNumber = RackCabinetCreateEditVM.InventoryNumber;
                rackcabinet.Users = RackCabinetCreateEditVM.Users;
                rackcabinet.ModifiedAt = DateTime.Now;
                await database.SaveChangesAsync();
                return Ok();
            }

            [Route("/rackcabinet/{id}")]
            [HttpDelete]
            public async Task<IActionResult> Delete(int id, [FromBody] RackCabinetCreateEditVM RackCabinetCreateEditVM)
            {
                DatabaseContext database = new();
                RackCabinet rackcabinet = await database.RackCabinets.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
                if (rackcabinet == null)
                {
                    return BadRequest("Nie ma komputera o podanym id {id}");
                }

                rackcabinet.ModifiedAt = DateTime.Now;
                await database.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
