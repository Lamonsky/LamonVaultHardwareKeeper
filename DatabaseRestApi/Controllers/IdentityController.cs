using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
