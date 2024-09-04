using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class LogsController : Controller
    {
        [Route(URLs.LOG)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<LogVM> list = await database.Logs
                .Select(item => new LogVM
                {
                    Id = item.Id,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.Email,
                    LogDate = (DateTime)item.LogDate,
                    Description = item.Description,
                    CreatedAt = (DateTime)item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email

                }).ToListAsync();
            return Json(list);
        }
        [Route(URLs.LOG_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            LogVM item = await database.Logs
                .Where(item => item.Id == id)
                .Select(item => new LogVM
                {
                    Id = item.Id,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName + " " + item.UsersNavigation.Email,
                    LogDate = (DateTime)item.LogDate,
                    Description = item.Description,
                    CreatedAt = (DateTime)item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,

                }).FirstAsync();
            return Json(item);
        }
        
        [Route(URLs.LOG)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LogCreateEditVM logCreateEditVM)
        {
            DatabaseContext database = new();
            database.Logs.Add(new()
            {
                Users = logCreateEditVM.Users,
                LogDate = logCreateEditVM.LogDate,
                Description = logCreateEditVM.Description,
                CreatedAt = logCreateEditVM.CreatedAt,
                CreatedBy = logCreateEditVM.CreatedBy,
            }); ;

            await database.SaveChangesAsync();
            return Ok();
        }

    }
}
