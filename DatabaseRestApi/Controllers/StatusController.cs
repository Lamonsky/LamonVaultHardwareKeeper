﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class StatusController : Controller
    {
        [Route(URLs.STATUS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<StatusVM> statusVM = await database.Statuses
                .Select(item => new StatusVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment
                    
                }).ToListAsync();
            return Json(statusVM);
        }
        [Route(URLs.STATUS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            database.Statuses.Add(new()
            {
                Name = statusCreateEditVM.Name,
                Comment = statusCreateEditVM.Comment,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.STATUS_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            Status? statuss = await database.Statuses.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            statuss.Name = statusCreateEditVM.Name;
            statuss.Comment = statusCreateEditVM.Comment;
            statuss.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.STATUS_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] StatusCreateEditVM statusCreateEditVM)
        {
            DatabaseContext database = new();
            Status? statuss = await database.Statuses.Where(item => item.Id == id).FirstOrDefaultAsync();
            if (statuss == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            statuss.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
