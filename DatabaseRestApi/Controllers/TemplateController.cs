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
    public class TemplateController : Controller
    {
        ///[Authorize]
        [Route(URLs.TEMPLATE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TemplatesVM> TemplatesVM = await database.Templates
                .Select(item => new TemplatesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    HTMLContent = item.Htmlcontent                    
                }).ToListAsync();
            return Json(TemplatesVM);
        }
        [Authorize]
        [Route(URLs.TEMPLATE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TemplatesVM TemplatesVM = await database.Templates
                .Where(item => item.Id == id)
                .Select(item => new TemplatesVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    HTMLContent = item.Htmlcontent 
                }).FirstAsync();
            return Json(TemplatesVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TEMPLATE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplatesVM templates)
        {
            DatabaseContext database = new();
            database.Templates.Add(new()
            {
                Name = templates.Name,
                Htmlcontent = templates.HTMLContent,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TEMPLATE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TemplatesVM templates)
        {
            DatabaseContext database = new();
            Template? statuss = await database.Templates.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            statuss.Name = templates.Name;
            statuss.Htmlcontent = templates.HTMLContent;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TEMPLATE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TemplatesVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            Template? statuss = await database.Templates.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            database.Templates.Remove(statuss);
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
