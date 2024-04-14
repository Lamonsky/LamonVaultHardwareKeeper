using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class ProjectController : Controller
    {
        [Route("/project")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ProjectVM> projectVMs = await database.Projects
                .Select(item => new ProjectVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Priority = item.Priority,
                    Code = item.Code,
                    Status = item.Status.Name,
                    ProjectType = item.ProjectTypeNavigation.Name,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    PlannedStartDate = item.PlannedStartDate,
                    PlannedEndDate = item.PlannedEndDate,
                    ActualStartDate = item.ActualStartDate,
                    ActualEndDate = item.ActualEndDate,
                    Description = item.Description,
                    Comment = item.Comment

                }).ToListAsync();
            return Json(projectVMs);
        }
        [Route("/project")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateEditVM projectCreateEditVM)
        {
            DatabaseContext database = new();
            database.Projects.Add(new()
            {
                Name = projectCreateEditVM.Name,
                Priority = projectCreateEditVM.Priority,
                Code = projectCreateEditVM.Code,
                StatusId = projectCreateEditVM.StatusId,
                ProjectType = projectCreateEditVM.ProjectType,
                Users = projectCreateEditVM.Users,
                PlannedStartDate = projectCreateEditVM.PlannedStartDate,
                PlannedEndDate = projectCreateEditVM.PlannedEndDate,
                ActualStartDate = projectCreateEditVM.ActualStartDate,
                ActualEndDate = projectCreateEditVM.ActualEndDate,
                Description = projectCreateEditVM.Description,
                Comment = projectCreateEditVM.Comment,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/project/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ProjectCreateEditVM projectCreateEditVM)
        {
            DatabaseContext database = new();
            Project project = await database.Projects.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (project == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            project.Name = projectCreateEditVM.Name;
            project.Priority = projectCreateEditVM.Priority;
            project.Code = projectCreateEditVM.Code;
            project.StatusId = projectCreateEditVM.StatusId;
            project.ProjectType = projectCreateEditVM.ProjectType;
            project.Users = projectCreateEditVM.Users;
            project.PlannedStartDate = projectCreateEditVM.PlannedStartDate;
            project.PlannedEndDate = projectCreateEditVM.PlannedEndDate;
            project.ActualStartDate = projectCreateEditVM.ActualStartDate;
            project.ActualEndDate = projectCreateEditVM.ActualEndDate;
            project.Description = projectCreateEditVM.Description;
            project.Comment = projectCreateEditVM.Comment;
            project.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/project/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ProjectCreateEditVM projectCreateEditVM)
        {
            DatabaseContext database = new();
            Project project = await database.Projects.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (project == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            project.StatusId = 99;
            project.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
