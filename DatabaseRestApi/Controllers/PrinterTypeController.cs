﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class PrinterTypeController : Controller
    {
        [Route(URLs.PRINTERTYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PrinterTypeVM> printerTypeVM = await database.PrinterTypes
                .Where(item => item.Status != 99)
                .Select(item => new PrinterTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(printerTypeVM);
        }
        [Route(URLs.PRINTERTYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.PrinterTypes.Add(new()
            {
                Name = printerTypeCreateEditVM.Name,
                Comment = printerTypeCreateEditVM.Comment,
                Status = printerTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERTYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterType? printerType = await database.PrinterTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerType.Name = printerTypeCreateEditVM.Name;
            printerType.Comment = printerTypeCreateEditVM.Comment;
            printerType.Status = printerTypeCreateEditVM.Status;
            printerType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.PRINTERTYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PrinterTypeCreateEditVM printerTypeCreateEditVM)
        {
            DatabaseContext database = new();
            PrinterType? printerType = await database.PrinterTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (printerType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printerType.Status = 99;
            printerType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
