using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PhoneController : Controller
    {
        [Route("/phones")]
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
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(phonesVMs);
        }
        [Route("/phones")]
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
                SerialNumber = phonesCreateEditVm?.SerialNumber,
                InventoryNumber = phonesCreateEditVm?.InventoryNumber,
                Users = phonesCreateEditVm.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phones/{id}")]
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
            phone.SerialNumber = phonesCreateEditVm?.SerialNumber;
            phone.InventoryNumber = phonesCreateEditVm?.InventoryNumber;
            phone.Users = phonesCreateEditVm.Users;
            phone.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phones/{id}")]
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
