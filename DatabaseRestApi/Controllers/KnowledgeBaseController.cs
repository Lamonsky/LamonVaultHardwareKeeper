﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        [Route(URLs.KNOWLEDGEBASE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<KnowledgeBaseVM> knowledgeBaseVM = await database.KnowledgeBases
                .Where(item => item.Status != 99)
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
        [Route(URLs.KNOWLEDGEBASE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            KnowledgeBaseVM knowledgeBaseVM = await database.KnowledgeBases
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new KnowledgeBaseVM
                {
                    Id = item.Id,
                    Category = item.CategoryNavigation.Name,
                    Subject = item.Subject,
                    Content = item.Content,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    Status = item.StatusNavigation.Name

                }).FirstAsync();
            return Json(knowledgeBaseVM);
        }
        [Route(URLs.KNOWLEDGEBASE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            KnowledgeBaseCreateEditVM knowledgeBaseVM = await database.KnowledgeBases
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new KnowledgeBaseCreateEditVM
                {
                    Id = item.Id,
                    Category = item.Category,
                    Subject = item.Subject,
                    Content = item.Content,
                    Users = item.Users,
                    Status = item.Status

                }).FirstAsync();
            return Json(knowledgeBaseVM);
        }
        [Route(URLs.KNOWLEDGEBASE)]
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

        [Route(URLs.KNOWLEDGEBASE_ID)]
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

        [Route(URLs.KNOWLEDGEBASE_ID)]
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
