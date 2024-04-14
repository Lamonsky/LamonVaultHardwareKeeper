using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class GroupTypeController : Controller
    {
        [Route("/grouptype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<GroupTypeVM> groupTypeVM = await database.GroupsTypes
                .Select(item => new GroupTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.StatusNavigation.Name,
                    Comment = item.Comment
                }).ToListAsync();
            return Json(groupTypeVM);
        }
        [Route("/grouptype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupTypeCreateEditVM groupTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.GroupsTypes.Add(new()
            {
                Name = groupTypeCreateEditVM.Name,
                Status = groupTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/grouptype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] GroupTypeCreateEditVM groupTypeCreateEditVM)
        {
            DatabaseContext database = new();
            GroupsType? groupTypes = await database.GroupsTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (groupTypes == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            groupTypes.Name = groupTypeCreateEditVM.Name;
            groupTypes.Status = groupTypeCreateEditVM.Status;
            groupTypes.Comment = groupTypeCreateEditVM.Comment;
            groupTypes.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/grouptype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] GroupTypeCreateEditVM groupTypeCreateEditVM)
        {
            DatabaseContext database = new();
            GroupsType? groupTypes = await database.GroupsTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (groupTypes == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            groupTypes.Status = 99;
            groupTypes.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
