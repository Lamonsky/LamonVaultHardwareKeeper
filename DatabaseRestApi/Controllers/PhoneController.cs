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
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(phonesVMs);
        }
        [Route(URLs.PHONE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PhonesVM phonesVMs = await database.Phones
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
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
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(phonesVMs);
        }
        [Route(URLs.PHONE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PhonesCreateEditVM phonesVMs = await database.Phones
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new PhonesCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    StatusId = item.StatusId,
                    PhoneType = item.PhoneType,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    SerialNumber = item.SerialNumber,
                    LocationId = item.LocationId,
                    InventoryNumber = item.InventoryNumber,
                    SimCard1 = item.SimCard1,
                    SimCard2 = item.SimCard2,
                    Users = item.Users
                }).FirstAsync();
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
                CreatedAt = DateTime.Now,
                CreatedBy = phonesCreateEditVm.CreatedBy
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
            phone.ModifiedBy = phonesCreateEditVm?.ModifiedBy;
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
