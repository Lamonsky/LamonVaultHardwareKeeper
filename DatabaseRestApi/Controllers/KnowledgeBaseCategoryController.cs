using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class KnowledgeBaseCategoryController : Controller
    {
        [Route("/knowledgebasecategory")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<KnowledgeBaseCategoryVM> knowledgeBaseCategoryVM = await database.KnowledgeBaseCategories
                .Select(item => new KnowledgeBaseCategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(knowledgeBaseCategoryVM);
        }
        [Route("/knowledgebasecategory")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KnowledgeBaseCategoryCreateEditVM knowledgeBaseCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            database.KnowledgeBaseCategories.Add(new()
            {
                Name = knowledgeBaseCategoryCreateEditVM.Name,
                Comment = knowledgeBaseCategoryCreateEditVM.Comment,
                Status = knowledgeBaseCategoryCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/knowledgebasecategory/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] KnowledgeBaseCategoryCreateEditVM knowledgeBaseCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            KnowledgeBaseCategory? knowledgeBaseCategory = await database.KnowledgeBaseCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (knowledgeBaseCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            knowledgeBaseCategory.Name = knowledgeBaseCategoryCreateEditVM.Name;
            knowledgeBaseCategory.Comment = knowledgeBaseCategoryCreateEditVM.Comment;
            knowledgeBaseCategory.Status = knowledgeBaseCategoryCreateEditVM.Status;
            knowledgeBaseCategory.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/knowledgebasecategory/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] KnowledgeBaseCategoryCreateEditVM knowledgeBaseCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            KnowledgeBaseCategory? knowledgeBaseCategory = await database.KnowledgeBaseCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (knowledgeBaseCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            knowledgeBaseCategory.Status = 99;
            knowledgeBaseCategory.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
