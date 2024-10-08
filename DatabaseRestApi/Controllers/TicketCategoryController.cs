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
    public class TicketCategoryController : Controller
    {
        [Authorize]
        [Route(URLs.TICKETCATEGORY)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<TicketCategoryVM> ticketCategoryVM = await database.TicketCategories
                .Where(item => item.Status != 99)
                .Select(item => new TicketCategoryVM
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
            return Json(ticketCategoryVM);
        }
        [Authorize]
        [Route(URLs.TICKETCATEGORY_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            TicketCategoryVM ticketCategoryVM = await database.TicketCategories
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TicketCategoryVM
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
            return Json(ticketCategoryVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TICKETCATEGORY_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            TicketCategoryCreateEditVM ticketCategoryVM = await database.TicketCategories
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new TicketCategoryCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(ticketCategoryVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TICKETCATEGORY)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            database.TicketCategories.Add(new()
            {
                Name = ticketCategoryCreateEditVM.Name,
                Comment = ticketCategoryCreateEditVM.Comment,
                Status = ticketCategoryCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = ticketCategoryCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TICKETCATEGORY_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            TicketCategory? ticketCategory = await database.TicketCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketCategory.Name = ticketCategoryCreateEditVM.Name;
            ticketCategory.Comment = ticketCategoryCreateEditVM.Comment;
            ticketCategory.Status = ticketCategoryCreateEditVM.Status;
            ticketCategory.ModifiedAt = DateTime.Now;
            ticketCategory.ModifiedBy = ticketCategoryCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.TICKETCATEGORY_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] TicketCategoryCreateEditVM ticketCategoryCreateEditVM)
        {
            DatabaseContext database = new();
            TicketCategory? ticketCategory = await database.TicketCategories.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (ticketCategory == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            ticketCategory.Status = 99;
            ticketCategory.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
