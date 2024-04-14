using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class ProjectTypeController : Controller
    {
        [Route("/projecttype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ProjectTypeVM> projectTypeVM = await database.ProjectTypes
                .Select(item => new ProjectTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(projectTypeVM);
        }
        [Route("/projecttype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectTypeCreateEditVM projectTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.ProjectTypes.Add(new()
            {
                Name = projectTypeCreateEditVM.Name,
                Comment = projectTypeCreateEditVM.Comment,
                Status = projectTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/projecttype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ProjectTypeCreateEditVM projectTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ProjectType? projectType = await database.ProjectTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (projectType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            projectType.Comment = projectTypeCreateEditVM.Comment;
            projectType.Name = projectTypeCreateEditVM.Name;
            projectType.Status = projectTypeCreateEditVM.Status;
            projectType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/projecttype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ProjectTypeCreateEditVM projectTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ProjectType? projectType = await database.ProjectTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (projectType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            projectType.Status = 99;
            projectType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
