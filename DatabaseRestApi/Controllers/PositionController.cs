﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PositionController : Controller
    {
        [Route(URLs.POSITION)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PositionVM> positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Select(item => new PositionVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(positionVM);
        }
        [Route(URLs.POSITION_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PositionVM positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PositionVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).FirstAsync();
            return Json(positionVM);
        }
        [Route(URLs.POSITION_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PositionCreateEditVM positionVM = await database.Positions
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new PositionCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(positionVM);
        }
        [Route(URLs.POSITION)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            database.Positions.Add(new()
            {
                Name = positionCreateEditVM.Name,
                Comment = positionCreateEditVM.Comment,
                Status = positionCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.POSITION_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            Position? position = await database.Positions.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (position == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            position.Name = positionCreateEditVM.Name;
            position.Comment = positionCreateEditVM.Comment;
            position.Status = positionCreateEditVM.Status;
            position.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.POSITION_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PositionCreateEditVM positionCreateEditVM)
        {
            DatabaseContext database = new();
            Position? position = await database.Positions.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (position == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            position.Status = 99;
            position.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
