using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class ContractTypeController : Controller
    {
        [Route("/contracttype")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ContractTypeVM> contractTypeVMs = await database.ContractTypes
                .Select(item => new ContractTypeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Comment = item.Comment,
                    Status = item.StatusNavigation.Name

                }).ToListAsync();
            return Json(contractTypeVMs);
        }
        [Route("/contracttype")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractTypeCreateEditVM contractTypeCreateEditVM)
        {
            DatabaseContext database = new();
            database.ContractTypes.Add(new()
            {
                Name = contractTypeCreateEditVM.Name,
                Comment = contractTypeCreateEditVM.Comment,
                Status = contractTypeCreateEditVM.Status,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/contracttype/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ContractTypeCreateEditVM contractTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ContractType? contractType = await database.ContractTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (contractType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            contractType.Name = contractTypeCreateEditVM.Name;
            contractType.Comment = contractTypeCreateEditVM.Comment;
            contractType.Status = contractTypeCreateEditVM.Status;
            contractType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/contracttype/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ContractTypeCreateEditVM contractTypeCreateEditVM)
        {
            DatabaseContext database = new();
            ContractType? contractType = await database.ContractTypes.Where(item => item.Id == id && item.Status != 99).FirstOrDefaultAsync();
            if (contractType == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            contractType.Status = 99;
            contractType.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
