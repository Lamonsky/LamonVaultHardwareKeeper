﻿using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Authorization;

namespace DatabaseRestApi.Controllers
{
    public class NetworkDeviceTypeController : Controller
    {
        [Authorize]
        [Route(URLs.NETWORKDEVICETYPE)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<NetworkDeviceTypeVM> networkDeviceTypeVM = await database.NetworkDeviceTypes
                .Where(item => item.Status != 99)
                .Select(item => new NetworkDeviceTypeVM
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
            return Json(networkDeviceTypeVM);
        }
        [Authorize]
        [Route(URLs.NETWORKDEVICETYPE_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            List<NetworkDeviceTypeVM> networkDeviceTypeVM = await database.NetworkDeviceTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new NetworkDeviceTypeVM
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
            return Json(networkDeviceTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICETYPE_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            NetworkDeviceTypeCreateEditVM networkDeviceTypeVM = await database.NetworkDeviceTypes
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new NetworkDeviceTypeCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(networkDeviceTypeVM);
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICETYPE)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NetworkDeviceTypeCreateEditVM networkDeviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.NetworkDeviceTypes.Add(new()
            {
                Name = networkDeviceTypeCreateEditVM.Name,
                Comment = networkDeviceTypeCreateEditVM.Comment,
                Status = networkDeviceTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = networkDeviceTypeCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICETYPE_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] NetworkDeviceTypeCreateEditVM networkDeviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDeviceType? networkDeviceType = await database.NetworkDeviceTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (networkDeviceType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            networkDeviceType.Name = networkDeviceTypeCreateEditVM.Name;
            networkDeviceType.Comment = networkDeviceTypeCreateEditVM.Comment;
            networkDeviceType.Status = networkDeviceTypeCreateEditVM.Status;
            networkDeviceType.ModifiedAt = DateTime.Now;
            networkDeviceType.ModifiedBy = networkDeviceTypeCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [Route(URLs.NETWORKDEVICETYPE_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] NetworkDeviceTypeCreateEditVM networkDeviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDeviceType? networkDeviceType = await database.NetworkDeviceTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (networkDeviceType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            networkDeviceType.Status = 99;
            networkDeviceType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
