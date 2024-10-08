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
    public class PrinterController : Controller
    {
        [Authorize]
        [Route(URLs.PRINTER)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<PrintersVM> PrintersVM = await database.Printers
                .Where(item => item.StatusId != 99)
                .Select(item => new PrintersVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    PrinterType = item.PrinterTypeNavigation.Name,
                    IpAddress = item.IpAddress,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(PrintersVM);
        }
        [Authorize]
        [Route(URLs.PRINTER_USER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetByUser(int id)
        {
            DatabaseContext database = new();
            List<PrintersVM> PrintersVM = await database.Printers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Users == id)
                .Select(item => new PrintersVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    PrinterType = item.PrinterTypeNavigation.Name,
                    IpAddress = item.IpAddress,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).ToListAsync();
            return Json(PrintersVM);
        }
        [Authorize]
        [Route(URLs.PRINTER_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            PrintersVM PrintersVM = await database.Printers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new PrintersVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    Manufacturer = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    PrinterType = item.PrinterTypeNavigation.Name,
                    IpAddress = item.IpAddress,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedByNavigation.FirstName + " " + item.CreatedByNavigation.LastName + " " + item.CreatedByNavigation.Email,
                    ModifiedAt = item.ModifiedAt,
                    ModifiedBy = item.ModifiedByNavigation.FirstName + " " + item.ModifiedByNavigation.LastName + " " + item.ModifiedByNavigation.Email
                }).FirstAsync();
            return Json(PrintersVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PRINTER_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            PrintersCreateEditVM PrintersVM = await database.Printers
                .Where(item => item.StatusId != 99)
                .Where(item => item.Id == id)
                .Select(item => new PrintersCreateEditVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    LocationId = item.LocationId,
                    StatusId = item.StatusId,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    PrinterType = item.PrinterType,
                    IpAddress = item.IpAddress,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.Users
                }).FirstAsync();
            return Json(PrintersVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PRINTER)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrintersCreateEditVM printerCreateEditVM)
        {
            DatabaseContext database = new();
            database.Printers.Add(new()
            {
                Name = printerCreateEditVM.Name,
                LocationId = printerCreateEditVM.LocationId,
                StatusId = printerCreateEditVM.StatusId,
                Manufacturer = printerCreateEditVM.Manufacturer,
                Model = printerCreateEditVM.Model,
                PrinterType = printerCreateEditVM.PrinterType,
                SerialNumber=printerCreateEditVM.SerialNumber,
                IpAddress = printerCreateEditVM.IpAddress,
                InventoryNumber = printerCreateEditVM.InventoryNumber,
                Users = printerCreateEditVM.Users,
                CreatedAt = DateTime.Now,
                CreatedBy = printerCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PRINTER_ID)]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] PrintersCreateEditVM printerCreateEditVM)
        {
            DatabaseContext database = new();
            Printer printer = await database.Printers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (printer == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printer.Name = printerCreateEditVM.Name;
            printer.LocationId = printerCreateEditVM.LocationId;
            printer.StatusId = printerCreateEditVM.StatusId;
            printer.Manufacturer = printerCreateEditVM.Manufacturer;
            printer.Model = printerCreateEditVM.Model;
            printer.PrinterType = printerCreateEditVM.PrinterType;
            printer.SerialNumber = printerCreateEditVM.SerialNumber;
            printer.InventoryNumber = printerCreateEditVM.InventoryNumber;
            printer.IpAddress = printerCreateEditVM.IpAddress;
            printer.Users = printerCreateEditVM.Users;
            printer.ModifiedAt = DateTime.Now;
            printer.ModifiedBy = printerCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.PRINTER_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PrintersCreateEditVM printerCreateEditVM)
        {
            DatabaseContext database = new();
            Printer printer = await database.Printers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (printer == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            printer.StatusId = 99;
            printer.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
