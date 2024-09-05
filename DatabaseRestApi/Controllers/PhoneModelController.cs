using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PhoneModelController : Controller
    {
        [Route(URLs.PHONEMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PhoneModelVM> phoneModelVM = await database.PhoneModels
                .Where(item => item.Status != 99)
                .Select(item => new PhoneModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(phoneModelVM);
        }
        [Route(URLs.PHONEMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PhoneModelVM phoneModelVM = await database.PhoneModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PhoneModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(phoneModelVM);
        }
        [Route(URLs.PHONEMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PhoneModelCreateEditVM phoneModelVM = await database.PhoneModels
                .Where(item => item.Id == id)
                .Where(item => item.Status != 99)
                .Select(item => new PhoneModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(phoneModelVM);
        }
        [Route(URLs.PHONEMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneModelCreateEditVM phoneModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.PhoneModels.Add(new()
            {
                Name = phoneModelCreateEditVM.Name,
                Comment = phoneModelCreateEditVM.Comment,
                Status = phoneModelCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = phoneModelCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PHONEMODEL_ID)]
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
            phoneModels.ModifiedBy = phoneModelCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PHONEMODEL_ID)]
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
