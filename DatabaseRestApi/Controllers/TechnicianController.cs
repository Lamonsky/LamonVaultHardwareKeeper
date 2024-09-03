using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class TechnicianController : Controller
    {
        [Route(URLs.TECHNICIAN)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TechnicianVM> technicianVMs = await database.Technicians
                .Where(item => item.Status != 99)
                .Select(item => new TechnicianVM
                {
                    Id = item.Id,
                    Users = item.Users.FirstName + " " + item.Users.LastName + " " + item.Users.InternalNumber,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(technicianVMs);
        }
        [Route(URLs.TECHNICIAN_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TechnicianVM technicianVMs = await database.Technicians
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TechnicianVM
                {
                    Id = item.Id,
                    Users = item.Users.FirstName + " " + item.Users.LastName + " " + item.Users.InternalNumber,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(technicianVMs);
        }
        [Route(URLs.TECHNICIAN_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            TechnicianCreateEditVM technicianVMs = await database.Technicians
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TechnicianCreateEditVM
                {
                    Id = item.Id,
                    UsersId = item.UsersId,
                    Status = item.Status
                }).FirstAsync();
            return Json(technicianVMs);
        }
        [Route(URLs.TECHNICIAN)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TechnicianCreateEditVM technicianCreateEditVM)
        {
            DatabaseContext database = new();
            database.Technicians.Add(new()
            {
                Status = technicianCreateEditVM.Status,
                UsersId = technicianCreateEditVM.UsersId,
                CreatedAt = DateTime.Now,
                CreatedBy = technicianCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.TECHNICIAN_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TechnicianCreateEditVM technicianCreateEditVM)
        {
            DatabaseContext database = new();
            Technician? technician = await database.Technicians.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (technician == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            technician.Status = technicianCreateEditVM.Status;
            technician.UsersId = technicianCreateEditVM.UsersId;
            technician.ModifiedBy = technicianCreateEditVM.ModifiedBy;
            technician.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.TECHNICIAN_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TechnicianCreateEditVM technicianCreateEditVM)
        {
            DatabaseContext database = new();
            Technician? technician = await database.Technicians.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (technician == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            technician.Status = 99;
            technician.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
