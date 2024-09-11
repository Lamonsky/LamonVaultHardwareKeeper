using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PageContentController : Controller
    {
        [Authorize]
        [Route(URLs.PAGECONTENT)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext context = new();
            List<PageContentVM> list = await context.PageContents
                .Select(item => new PageContentVM()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content
                }).ToListAsync();
            return Json(list);
        }
        [Authorize]
        [Route(URLs.PAGECONTENT_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByTitle(string id)
        {
            DatabaseContext context = new();
            PageContentVM list = await context.PageContents
                .Where(item => item.Title == id)
                .Select(item => new PageContentVM()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content
                }).FirstOrDefaultAsync();
            return Json(list);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PAGECONTENT_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetPageContent(int id)
        {
            DatabaseContext context = new();
            PageContentCreateEditVM list = await context.PageContents
                .Where(item => item.Id == id)
                .Select(item => new PageContentCreateEditVM()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content
                }).FirstOrDefaultAsync();
            return Json(list);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PAGECONTENT)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PageContentCreateEditVM PageContentCreateEditVM)
        {
            DatabaseContext databaseContext = new();
            databaseContext.PageContents.Add(new()
            {
                Title = PageContentCreateEditVM.Title,
                Content = PageContentCreateEditVM.Content
            });

            await databaseContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PAGECONTENT_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] PageContentCreateEditVM PageContentCreateEditVM)
        {
            DatabaseContext databaseContext = new();
            PageContent PageContent = await databaseContext.PageContents.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (PageContent == null)
            {
                return BadRequest("Nie ma obiektu o podanym id {id}");
            }
            PageContent.Title = PageContentCreateEditVM.Title;
            PageContent.Content = PageContentCreateEditVM.Content;


            await databaseContext.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PAGECONTENT_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PageContentCreateEditVM PageContentCreateEditVM)
        {
            DatabaseContext databaseContext = new();
            PageContent PageContent = await databaseContext.PageContents.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (PageContent == null)
            {
                return BadRequest("Nie ma obiektu o podanym id {id}");
            }
            databaseContext.PageContents.Remove(PageContent);

            await databaseContext.SaveChangesAsync();
            return Ok();
        }
    }
}