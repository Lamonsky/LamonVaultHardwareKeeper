using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PhoneModelController : Controller
    {
        [Route("/phonemodel")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PhoneModelVM> phoneModelVM = await database.PhoneModels
                .Select(item => new PhoneModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(phoneModelVM);
        }
        [Route("/phonemodel")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneModelCreateEditVM phoneModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.PhoneModels.Add(new()
            {
                Name = phoneModelCreateEditVM.Name,
                Comment = phoneModelCreateEditVM.Comment,
                Status = phoneModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phonemodel/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PhoneModelCreateEditVM phoneModelCreateEditVM)
        {
            DatabaseContext database = new();
            PhoneModel? phoneModels = await database.PhoneModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (phoneModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phoneModels.Name = phoneModelCreateEditVM.Name;
            phoneModels.Comment = phoneModelCreateEditVM.Comment;
            phoneModels.Status = phoneModelCreateEditVM.Status;
            phoneModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/phonemodel/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PhoneModelCreateEditVM phoneModelCreateEditVM)
        {
            DatabaseContext database = new();
            PhoneModel? phoneModels = await database.PhoneModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (phoneModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            phoneModels.Status = 99;
            phoneModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
