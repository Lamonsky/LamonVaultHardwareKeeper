using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PhoneController : Controller
    {
        [Route(URLs.PHONE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PhonesVM> phonesVMs = await database.Phones
                .Where(item => item.StatusId != 99)
                .Select(item => new PhonesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status.Name,
                    PhoneType = item.PhoneTypeNavigation.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    Location = item.Location.Name,
                    InventoryNumber = item.InventoryNumber,
                    SimCard1 = item.SimCard1Navigation.PhoneNumber + " " + item.SimCard1Navigation.SerialNumber + " " + item.SimCard1Navigation.InventoryNumber,
                    SimCard2 = item.SimCard2Navigation.PhoneNumber + " " + item.SimCard2Navigation.SerialNumber + " " + item.SimCard2Navigation.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(phonesVMs);
        }
        [Route(URLs.PHONE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhonesCreateEditVM phonesCreateEditVm)
        {
            DatabaseContext database = new();
            database.Phones.Add(new()
            {
                Name = phonesCreateEditVm.Name,
                StatusId = phonesCreateEditVm?.StatusId,
                PhoneType = phonesCreateEditVm?.PhoneType,
                Manufacturer = phonesCreateEditVm?.Manufacturer,
                Model = phonesCreateEditVm?.Model,
                LocationId = phonesCreateEditVm?.LocationId,
                SerialNumber = phonesCreateEditVm?.SerialNumber,
                SimCard1 = phonesCreateEditVm?.SimCard1,
                SimCard2 = phonesCreateEditVm?.SimCard2,
                InventoryNumber = phonesCreateEditVm?.InventoryNumber,
                Users = phonesCreateEditVm.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PHONE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PhonesCreateEditVM phonesCreateEditVm)
        {
            DatabaseContext database = new();
            Phone phone = await database.Phones.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (phone == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phone.Name = phonesCreateEditVm.Name;
            phone.StatusId = phonesCreateEditVm?.StatusId;
            phone.PhoneType = phonesCreateEditVm?.PhoneType;
            phone.Manufacturer = phonesCreateEditVm?.Manufacturer;
            phone.Model = phonesCreateEditVm?.Model;
            phone.LocationId = phonesCreateEditVm?.LocationId;
            phone.SerialNumber = phonesCreateEditVm?.SerialNumber;
            phone.InventoryNumber = phonesCreateEditVm?.InventoryNumber;
            phone.SimCard1 = phonesCreateEditVm?.SimCard1;
            phone.SimCard2 = phonesCreateEditVm?.SimCard2;
            phone.Users = phonesCreateEditVm.Users;
            phone.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PHONE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PhonesCreateEditVM phonesCreateEditVm)
        {
            DatabaseContext database = new();
            Phone phone = await database.Phones.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (phone == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phone.StatusId = 99;
            phone.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
