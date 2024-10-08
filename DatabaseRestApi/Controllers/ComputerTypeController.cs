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
    public class ComputerTypeController : Controller
    {
        [Authorize]
        [Route(URLs.COMPUTERTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ComputerTypeVM> computerTypeVM = await database.ComputerTypes
                .Where(item => item.Status != 99)
                .Select(item => new ComputerTypeVM
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
            return Json(computerTypeVM);
        }
        [Authorize]
        [Route(URLs.COMPUTERTYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            ComputerTypeVM computerTypeVM = await database.ComputerTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new ComputerTypeVM
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
            return Json(computerTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.COMPUTERTYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            ComputerTypeCreateEditVM computerTypeVM = await database.ComputerTypes
                .Where(item => item.Id == id)
                .Select(item => new ComputerTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstOrDefaultAsync();
            return Json(computerTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.COMPUTERTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComputerTypeCreateEditVM computerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.ComputerTypes.Add(new()
            {
                Name = computerTypeCreateEditVM.Name,
                Comment = computerTypeCreateEditVM.Comment,
                Status = computerTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = computerTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.COMPUTERTYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ComputerTypeCreateEditVM computerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ComputerType? computerTypes = await database.ComputerTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (computerTypes == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            computerTypes.Name = computerTypeCreateEditVM.Name;
            computerTypes.Comment = computerTypeCreateEditVM.Comment;
            computerTypes.Status = computerTypeCreateEditVM.Status;
            computerTypes.ModifiedAt = DateTime.Now;
            computerTypes.ModifiedBy = computerTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.COMPUTERTYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ComputerTypeCreateEditVM computerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ComputerType? computerTypes = await database.ComputerTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (computerTypes == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            computerTypes.Status = 99;
            computerTypes.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
