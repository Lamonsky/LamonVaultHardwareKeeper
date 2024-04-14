using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class NetworkDeviceModelController : Controller
    {
        [Route("/networkdevicemodel")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<NetworkDeviceModelVM> networkDeviceModelVM = await database.NetworkDeviceModels
                .Select(item => new NetworkDeviceModelVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(networkDeviceModelVM);
        }
        [Route("/networkdevicemodel")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NetworkDeviceModelCreateEditVM networkDeviceModelCreateEditVM)
        {
            DatabaseContext database = new();
            database.NetworkDeviceModels.Add(new()
            {
                Name = networkDeviceModelCreateEditVM.Name,
                Comment = networkDeviceModelCreateEditVM.Comment,
                Status = networkDeviceModelCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevicemodel/{id}")]
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
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/networkdevicemodel/{id}")]
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
