using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace DatabaseRestApi.Controllers
{
    public class NetworkDeviceModelController : Controller
    {
        [Route(URLs.NETWORKDEVICEMODEL)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<NetworkDeviceModelVM> networkDeviceModelVM = await database.NetworkDeviceModels
                .Where(item => item.Status != 99)
                .Select(item => new NetworkDeviceModelVM
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
            return Json(networkDeviceModelVM);
        }
        [Route(URLs.NETWORKDEVICEMODEL_ID)]
        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            DatabaseContext database = new();
            NetworkDeviceModelVM networkDeviceModelVM = await database.NetworkDeviceModels
                .Where(item => item.Id == id)
                .Where(item => item.Status != 99)
                .Select(item => new NetworkDeviceModelVM
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
            return Json(networkDeviceModelVM);
        }
        [Route(URLs.NETWORKDEVICEMODEL_CEVM_ID)]
        [HttpGet]
        public async Task<IActionResult> GetEditItem(int id)
        {
            DatabaseContext database = new();
            NetworkDeviceModelCreateEditVM networkDeviceModelVM = await database.NetworkDeviceModels
                .Where(item => item.Status != 99)
                .Where(item => item.Id == id)
                .Select(item => new NetworkDeviceModelCreateEditVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.Status

                }).FirstAsync();
            return Json(networkDeviceModelVM);
        }
        [Route(URLs.NETWORKDEVICEMODEL)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NetworkDeviceModelCreateEditVM networkDeviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.NetworkDeviceModels.Add(new()
            {
                Name = networkDeviceModelCreateEditVM.Name,
                Comment = networkDeviceModelCreateEditVM.Comment,
                Status = networkDeviceModelCreateEditVM.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = networkDeviceModelCreateEditVM.CreatedBy,
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.NETWORKDEVICEMODEL_ID)]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] NetworkDeviceModelCreateEditVM networkDeviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDeviceModel? networkDeviceModels = await database.NetworkDeviceModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (networkDeviceModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            networkDeviceModels.Name = networkDeviceModelCreateEditVM.Name;
            networkDeviceModels.Comment = networkDeviceModelCreateEditVM.Comment;
            networkDeviceModels.Status = networkDeviceModelCreateEditVM.Status;
            networkDeviceModels.ModifiedAt = DateTime.Now;
            networkDeviceModels.ModifiedBy = networkDeviceModelCreateEditVM.ModifiedBy;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route(URLs.NETWORKDEVICEMODEL_ID)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] NetworkDeviceModelCreateEditVM networkDeviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            NetworkDeviceModel? networkDeviceModels = await database.NetworkDeviceModels.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (networkDeviceModels == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            networkDeviceModels.Status = 99;
            networkDeviceModels.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
