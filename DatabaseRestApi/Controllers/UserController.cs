using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DatabaseRestApi.Controllers
{
    public class UserController : Controller
    {
        [Route("/user")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<UserVM> userVM= await database.Users
                .Select(item => new UserVM
                {
                    Id = item.Id,
                    Usersname = item.Usersname,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Location = item.Location.Name,
                    IsActive = item.IsActive,
                    Phone1 = item.Phone1,
                    Phone2 = item.Phone2,
                    InternalNumber = item.InternalNumber,
                    Position = item.PositionNavigation.Name

                }).ToListAsync();
            return Json(userVM);
        }
        [Route("/user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateEditVM userCreateEditVM)
        {
            DatabaseContext database = new();
            database.Users.Add(new()
            {
                Usersname = userCreateEditVM.Usersname,
                FirstName = userCreateEditVM.FirstName,
                LastName = userCreateEditVM.LastName,
                Email = userCreateEditVM.Email,
                LocationId = userCreateEditVM.LocationId,
                IsActive=userCreateEditVM.IsActive,
                Phone1 = userCreateEditVM.Phone1,
                Phone2 = userCreateEditVM.Phone2,
                InternalNumber = userCreateEditVM.InternalNumber,
                Password = userCreateEditVM.Password,
                Position = userCreateEditVM.Position,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/user/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] UserCreateEditVM userCreateEditVM)
        {
            DatabaseContext database = new();
            User user = await database.Users.Where(item => item.Id == id && item.IsActive == true).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            user.Usersname = userCreateEditVM.Usersname;
            user.FirstName = userCreateEditVM.FirstName;
            user.LastName = userCreateEditVM.LastName;
            user.Email = userCreateEditVM.Email;
            user.LocationId = userCreateEditVM.LocationId;
            user.IsActive=userCreateEditVM.IsActive;
            user.Phone1 = userCreateEditVM.Phone1;
            user.Phone2 = userCreateEditVM.Phone2;
            user.InternalNumber = userCreateEditVM.InternalNumber;
            user.Password = userCreateEditVM.Password;
            user.Position = userCreateEditVM.Position;
            user.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/user/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] UserCreateEditVM userCreateEditVM)
        {
            DatabaseContext database = new();
            User? user = await database.Users.Where(item => item.Id == id && item.IsActive == true).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            user.IsActive = false;
            user.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
