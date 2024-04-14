using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PhoneTypeController : Controller
    {
        [Route("/phonetype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PhoneTypeVM> phoneTypeVM = await database.PhoneTypes
                .Select(item => new PhoneTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(phoneTypeVM);
        }
        [Route("/phonetype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneTypeCreateEditVM phoneTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.PhoneTypes.Add(new()
            {
                Name = phoneTypeCreateEditVM.Name,
                Comment = phoneTypeCreateEditVM.Comment,
                Status = phoneTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phonetype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PhoneTypeCreateEditVM phoneTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PhoneType? phoneType = await database.PhoneTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (phoneType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phoneType.Name = phoneTypeCreateEditVM.Name;
            phoneType.Comment = phoneTypeCreateEditVM.Comment;
            phoneType.Status = phoneTypeCreateEditVM.Status;
            phoneType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phonetype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PhoneTypeCreateEditVM phoneTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PhoneType? phoneType = await database.PhoneTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (phoneType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phoneType.Status = 99;
            phoneType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
