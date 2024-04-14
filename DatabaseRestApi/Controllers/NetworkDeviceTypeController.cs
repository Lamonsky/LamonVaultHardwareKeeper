using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class NetworkDeviceTypeController : Controller
    {
        [Route("/networkdevicetype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<NetworkDeviceTypeVM> networkDeviceTypeVM = await database.NetworkDeviceTypes
                .Select(item => new NetworkDeviceTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(networkDeviceTypeVM);
        }
        [Route("/networkdevicetype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NetworkDeviceTypeCreateEditVM networkDeviceTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.NetworkDeviceTypes.Add(new()
            {
                Name = networkDeviceTypeCreateEditVM.Name,
                Comment = networkDeviceTypeCreateEditVM.Comment,
                Status = networkDeviceTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevicetype/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevicetype/{id}")]
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
