using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class PhoneTypeController : Controller
    {
        [Authorize]
        [Route(URLs.PHONETYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PhoneTypeVM> phoneTypeVM = await database.PhoneTypes
                .Where(item => item.Status != 99)
                .Select(item => new PhoneTypeVM
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
            return Json(phoneTypeVM);
        }
        [Authorize]
        [Route(URLs.PHONETYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PhoneTypeVM phoneTypeVM = await database.PhoneTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PhoneTypeVM
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
            return Json(phoneTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PHONETYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PhoneTypeCreateEditVM phoneTypeVM = await database.PhoneTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PhoneTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(phoneTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PHONETYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneTypeCreateEditVM phoneTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.PhoneTypes.Add(new()
            {
                Name = phoneTypeCreateEditVM.Name,
                Comment = phoneTypeCreateEditVM.Comment,
                Status = phoneTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = phoneTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PHONETYPE_ID)]
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
            phoneType.ModifiedBy = phoneTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PHONETYPE_ID)]
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
