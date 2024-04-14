using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class TechnicianController : Controller
    {
        [Route("/technician")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TechnicianVM> technicianVMs = await database.Technicians
                .Select(item => new TechnicianVM
                {
                    Id = item.Id,
                    Users = item.Users.FirstName + " " + item.Users.LastName + " " + item.Users.InternalNumber,
                    Status = item.StatusNavigation.Name
                }).ToListAsync();
            return Json(technicianVMs);
        }
        [Route("/technician")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TechnicianCreateEditVM technicianCreateEditVM)
        {
            DatabaseContext database = new();
            database.Technicians.Add(new()
            {
                Status = technicianCreateEditVM.Status,
                UsersId = technicianCreateEditVM.UsersId,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/technician/{id}")]
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
            technician.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/technician/{id}")]
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
