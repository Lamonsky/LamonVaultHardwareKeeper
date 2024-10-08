﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class MonitorModelController : Controller
    {
        [Authorize]
        [Route(URLs.MONITORMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<MonitorModelVM> monitorModelVM = await database.MonitorModels
                .Where(item => item.Status != 99)
                .Select(item => new MonitorModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).ToListAsync();
            return Json(monitorModelVM);
        }
        [Authorize]
        [Route(URLs.MONITORMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            MonitorModelVM monitorModelVM = await database.MonitorModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new MonitorModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email

                }).FirstAsync();
            return Json(monitorModelVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.MONITORMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            MonitorModelCreateEditVM monitorModelVM = await database.MonitorModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new MonitorModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(monitorModelVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.MONITORMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.MonitorModels.Add(new()
            {
                Name = monitorModelCreateEditVM.Name,
                Comment = monitorModelCreateEditVM.Comment,
                Status = monitorModelCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = monitorModelCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.MONITORMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorModel? monitorModels = await database.MonitorModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorModels.Name = monitorModelCreateEditVM.Name;
            monitorModels.Comment = monitorModelCreateEditVM.Comment;
            monitorModels.Status = monitorModelCreateEditVM.Status;
            monitorModels.ModifiedAt = DateTime.Now;
            monitorModels.ModifiedBy = monitorModelCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.MONITORMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] MonitorModelCreateEditVM monitorModelCreateEditVM)
        {
            DatabaseContext database = new();
            MonitorModel? monitorModels = await database.MonitorModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (monitorModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitorModels.Status = 99;
            monitorModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
