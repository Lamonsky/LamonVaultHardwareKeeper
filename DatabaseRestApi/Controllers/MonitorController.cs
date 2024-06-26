﻿using Data;
using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace DatabaseRestApi.Controllers
{
    public class MonitorController : Controller
    {
        [Route(URLs.MONITORS)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<MonitorsVM> monitorsVMs = await database.Monitors
                .Where(item => item.StatusId != 99)
                .Select(item => new MonitorsVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location.Name,
                    Status = item.Status.Name,
                    MonitorType = item.MonitorTypeNavigation.Name,
                    ManufacturerName = item.ManufacturerNavigation.Name,
                    Model = item.ModelNavigation.Name,
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(monitorsVMs);
        }

        [Route(URLs.MONITORS)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonitorsCreateEditVM monitorsCreateEditVM)
        {
            DatabaseContext database = new();
            database.Monitors.Add(new()
            {
                Name = monitorsCreateEditVM.Name,
                LocationId = monitorsCreateEditVM.LocationId,
                StatusId = monitorsCreateEditVM.StatusId,
                MonitorType = monitorsCreateEditVM.MonitorType,
                Manufacturer = monitorsCreateEditVM.Manufacturer,
                Model = monitorsCreateEditVM.Model,
                SerialNumber = monitorsCreateEditVM.SerialNumber,
                InventoryNumber = monitorsCreateEditVM.InventoryNumber,
                Users = monitorsCreateEditVM.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MONITORS_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] MonitorsCreateEditVM monitorsCreateEditVM)
        {
            DatabaseContext database = new();
            Models.Monitor? monitor = await database.Monitors.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if(monitor == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            monitor.Name = monitorsCreateEditVM.Name;
            monitor.LocationId = monitorsCreateEditVM.LocationId;
            monitor.StatusId = monitorsCreateEditVM.StatusId;
            monitor.MonitorType = monitorsCreateEditVM.MonitorType;
            monitor.Manufacturer = monitorsCreateEditVM.Manufacturer;
            monitor.Model = monitorsCreateEditVM.Model;
            monitor.SerialNumber = monitorsCreateEditVM.SerialNumber;
            monitor.InventoryNumber = monitorsCreateEditVM.InventoryNumber;
            monitor.Users = monitorsCreateEditVM.Users;
            monitor.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.MONITORS_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] MonitorsCreateEditVM monitorsCreateEditVM)
        {
            DatabaseContext database = new();
            Models.Monitor? monitor = await database.Monitors.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (monitor == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            monitor.StatusId = 99;
            monitor.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

    }
}
