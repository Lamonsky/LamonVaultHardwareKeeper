using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        [Route("/knowledgebase")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<KnowledgeBaseVM> knowledgeBaseVM = await database.KnowledgeBases
                .Select(item => new KnowledgeBaseVM
                {
                    Id = item.Id,
                    Category = item.CategoryNavigation.Name,
                    Subject = item.Subject,
                    Content = item.Content,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(knowledgeBaseVM);
        }
        [Route("/knowledgebase")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KnowledgeBaseCreateEditVM knowledgeBaseCreateEditVM)
        {
            DatabaseContext database = new();
            database.KnowledgeBases.Add(new()
            {
                Subject = knowledgeBaseCreateEditVM.Subject,
                Category = knowledgeBaseCreateEditVM.Category,
                Content = knowledgeBaseCreateEditVM.Content,
                Users = knowledgeBaseCreateEditVM.Users,
                Status = knowledgeBaseCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/knowledgebase/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] KnowledgeBaseCreateEditVM knowledgeBaseCreateEditVM)
        {
            DatabaseContext database = new();
            KnowledgeBase? knowledgebase = await database.KnowledgeBases.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (knowledgebase == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            knowledgebase.Subject = knowledgeBaseCreateEditVM.Subject;
            knowledgebase.Category = knowledgeBaseCreateEditVM.Category;
            knowledgebase.Content = knowledgeBaseCreateEditVM.Content;
            knowledgebase.Users = knowledgeBaseCreateEditVM.Users;
            knowledgebase.Status = knowledgeBaseCreateEditVM.Status;
            knowledgebase.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/knowledgebase/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] KnowledgeBaseCreateEditVM knowledgeBaseCreateEditVM)
        {
            DatabaseContext database = new();
            KnowledgeBase? knowledgebase = await database.KnowledgeBases.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (knowledgebase == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            knowledgebase.Status = 99;
            knowledgebase.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
