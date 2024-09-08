using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class SimCardController : Controller
    {
        [Route(URLs.SIMCARD)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<SimCardsVM> simcardsVMs = await database.SimCards
                .Where(item => item.StatusId != 99)
                .Select(item => new SimCardsVM
                {
                    Id = item.Id,
                    PinCode = item.PinCode,
                    PukCode = item.PukCode,
                    Component = item.ComponentNavigation.Name,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(simcardsVMs);
        }
        [Route(URLs.SIMCARD_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<SimCardsVM> simcardsVMs = await database.SimCards
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new SimCardsVM
                {
                    Id = item.Id,
                    PinCode = item.PinCode,
                    PukCode = item.PukCode,
                    Component = item.ComponentNavigation.Name,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(simcardsVMs);
        }
        [Route(URLs.SIMCARD_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            SimCardsVM simcardsVMs = await database.SimCards
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimCardsVM
                {
                    Id = item.Id,
                    PinCode = item.PinCode,
                    PukCode = item.PukCode,
                    Component = item.ComponentNavigation.Name,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(simcardsVMs);
        }
        [Route(URLs.SIMCARD_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            SimCardsCreateEditVM simcardsVMs = await database.SimCards
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new SimCardsCreateEditVM
                {
                    Id = item.Id,
                    PinCode = item.PinCode,
                    PukCode = item.PukCode,
                    Component = item.Component,
                    PhoneNumber = item.PhoneNumber,
                    StatusId = item.StatusId,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users,
                }).FirstAsync();
            return Json(simcardsVMs);
        }
        [Route(URLs.SIMCARD)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SimCardsCreateEditVM simCardsCreateEditVM)
        {
            DatabaseContext database = new();
            database.SimCards.Add(new()
            {
                PinCode = simCardsCreateEditVM.PinCode,
                PukCode = simCardsCreateEditVM.PukCode,
                Component = simCardsCreateEditVM.Component,
                PhoneNumber = simCardsCreateEditVM.PhoneNumber,
                StatusId = simCardsCreateEditVM?.StatusId,
                SerialNumber = simCardsCreateEditVM?.SerialNumber,
                InventoryNumber = simCardsCreateEditVM?.InventoryNumber,
                Users = simCardsCreateEditVM.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = simCardsCreateEditVM?.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SIMCARD_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] SimCardsCreateEditVM simCardsCreateEditVM)
        {
            DatabaseContext database = new();
            SimCard simcard = await database.SimCards.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (simcard == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simcard.PinCode = simCardsCreateEditVM.PinCode;
            simcard.PukCode = simCardsCreateEditVM.PukCode;
            simcard.Component = simCardsCreateEditVM.Component;
            simcard.PhoneNumber = simCardsCreateEditVM.PhoneNumber;
            simcard.StatusId = simCardsCreateEditVM?.StatusId;
            simcard.SerialNumber = simCardsCreateEditVM?.SerialNumber;
            simcard.InventoryNumber = simCardsCreateEditVM?.InventoryNumber;
            simcard.Users = simCardsCreateEditVM.Users;
            simcard.ModifiedBy = simCardsCreateEditVM?.ModifiedBy;
            simcard.StatusId = simCardsCreateEditVM.StatusId;
            simcard.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.SIMCARD_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] SimCardsCreateEditVM simCardsCreateEditVM)
        {
            DatabaseContext database = new();
            SimCard simcard = await database.SimCards.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (simcard == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            simcard.StatusId = 99;
            simcard.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
