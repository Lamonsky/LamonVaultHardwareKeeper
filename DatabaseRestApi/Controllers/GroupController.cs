using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class GroupController : Controller
    {
        [Route("/group")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<GroupVM> groupVMs = await database.Groups
                .Select(item => new GroupVM
                {
                    Id = item.Id,
                    Users = item.Users.FirstName + " " + item.Users.LastName + " " + item.Users.InternalNumber,
                    GroupName = item.GroupType.Name,

                }).ToListAsync();
            return Json(groupVMs);
        }
        [Route("/group")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateEditVM groupCreateEditVM)
        {
            DatabaseContext database = new();
            database.Groups.Add(new()
            {
                GroupTypeId = groupCreateEditVM.GroupTypeId,
                UsersId = groupCreateEditVM.UsersId,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/group/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] GroupCreateEditVM groupCreateEditVM)
        {
            DatabaseContext database = new();
            Group? group = await database.Groups.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (group == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            group.UsersId = groupCreateEditVM.UsersId;
            group.GroupTypeId = groupCreateEditVM.GroupTypeId;
            group.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/group/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] GroupCreateEditVM groupCreateEditVM)
        {
            DatabaseContext database = new();
            Group? group = await database.Groups.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (group == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            group.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
