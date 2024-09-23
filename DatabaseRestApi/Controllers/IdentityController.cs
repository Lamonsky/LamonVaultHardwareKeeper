using Data;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseRestApi.Controllers
{
    public class IdentityController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public UserManager<IdentityUser> UserManager { get; set; }
        public IdentityController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        [Authorize]
        [HttpGet]
        [Route(URLs.IDENTITY_CREATE_ROLE)]
        public async Task<IActionResult> CreateRole(string name)
        {
            if(!await RoleManager.RoleExistsAsync(name))
            {
                await RoleManager.CreateAsync(new IdentityRole(name));
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(URLs.IDENTITY_CHECK_USER_ROLE)]
        public async Task<IActionResult> UsersWithRoles()
        {
            List<UsersWithRolesVM> usersWithRoles = new List<UsersWithRolesVM>();

            foreach (var user in UserManager.Users.ToList())
            {
                var roles = await UserManager.GetRolesAsync(user);

                usersWithRoles.Add(new UsersWithRolesVM
                {
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return Json(usersWithRoles);

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(URLs.IDENTITY_ADD_USER_TO_ROLE)]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);
            if(!await RoleManager.RoleExistsAsync(roleName))
            {
                throw new Exception($"Invalid role name: {roleName}");
            }
            if(identityUser != null)
            {
                if(!await UserManager.IsInRoleAsync(identityUser, roleName))
                {
                    await UserManager.AddToRoleAsync(identityUser, roleName);
                }
            }
            else
            {
                throw new Exception($"Invalid user email: {email}");
            }
            return Ok();
        }
        [Authorize(Roles = "sa")]
        [HttpGet]
        [Route(URLs.IDENTITY_ADD_USER_TO_ADMIN_ROLE)]
        public async Task<IActionResult> AddUserToRole(string email)
        {
            string roleName = "Admin";
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);
            if(!await RoleManager.RoleExistsAsync(roleName))
            {
                throw new Exception($"Invalid role name: {roleName}");
            }
            if(identityUser != null)
            {
                if(!await UserManager.IsInRoleAsync(identityUser, roleName))
                {
                    await UserManager.AddToRoleAsync(identityUser, roleName);
                }
            }
            else
            {
                throw new Exception($"Invalid user email: {email}");
            }
            return Ok();
        }
        [HttpGet]
        [Route(URLs.IDENTITY_CHECK_USER_ADMIN_ROLE)]
        public async Task<IActionResult> CheckUserAdminRole(string email)
        {
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return NotFound(new { message = $"User with email {email} not found." });
            }

            bool isInRole = await UserManager.IsInRoleAsync(identityUser, "Admin");

            return Ok(new {isInRole});
        }
        [Authorize(Roles = "sa")]
        [HttpGet]
        [Route(URLs.IDENTITY_REMOVE_USER_FROM_ADMIN_ROLE)]
        public async Task<IActionResult> RemoveUserFromAdmin(string email)
        {
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return NotFound(new { message = $"User with email {email} not found." });
            }

            bool isInRole = await UserManager.IsInRoleAsync(identityUser, "Admin");

            if (isInRole)
            {
                var result = await UserManager.RemoveFromRoleAsync(identityUser, "Admin");

                return Ok(new { message = $"Użytkownik usunięty z roli Admina." });
            }

            return Ok(new { message = $"Użytkownik nie jest Adminem" });
        }

        [HttpGet]
        [Route(URLs.IDENTITY_CHECK_USER_SUPERADMIN_ROLE)]
        public async Task<IActionResult> CheckUserSuperAdminRole(string email)
        {
            IdentityUser? identityUser = await UserManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                return NotFound(new { message = $"User with email {email} not found." });
            }

            bool isInRole = await UserManager.IsInRoleAsync(identityUser, "sa");

            return Ok(new {isInRole});
        }
    }
}
