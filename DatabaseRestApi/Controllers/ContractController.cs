using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class ContractController : Controller
    {
        [Route("/contract")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new();
            List<ContractVM> contractVM = await database.Contracts
                .Select(item => new ContractVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status.Name,
                    Location = item.Location.Name,
                    ContractType = item.ContractTypeNavigation.Name,
                    StartDate = item.StartDate,
                    ContractDuration = item.ContractDuration.ToString(),
                    AccountNumber = item.AccountNumber,
                    PaymentTerms = item.PaymentTerms.ToString(),
                    IsRenewable = item.IsRenewable,
                    User = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName,

                }).ToListAsync();
            return Json(contractVM);
        }
        [Route("/contract")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractCreateEditVM contractCreateEditVM)
        {
            DatabaseContext database = new();
            database.Contracts.Add(new()
            {
                Id = contractCreateEditVM.Id,
                Name = contractCreateEditVM.Name,
                StatusId = contractCreateEditVM.StatusId,
                LocationId = contractCreateEditVM.LocationId,
                ContractType = contractCreateEditVM.ContractType,
                StartDate = contractCreateEditVM.StartDate,
                ContractDuration = contractCreateEditVM.ContractDuration,
                AccountNumber = contractCreateEditVM.AccountNumber,
                PaymentTerms = contractCreateEditVM.PaymentTerms,
                IsRenewable = contractCreateEditVM.IsRenewable,
                Users = contractCreateEditVM.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/contract/{id}")]
        [HttpPut]
        public async Task<IActionResult> Create(int id, [FromBody] ContractCreateEditVM contractCreateEditVM)
        {
            DatabaseContext database = new();
            Contract? contract = await database.Contracts.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (contract == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            contract.Id = contractCreateEditVM.Id;
            contract.Name = contractCreateEditVM.Name;
            contract.StatusId = contractCreateEditVM.StatusId;
            contract.LocationId = contractCreateEditVM.LocationId;
            contract.ContractType = contractCreateEditVM.ContractType;
            contract.StartDate = contractCreateEditVM.StartDate;
            contract.ContractDuration = contractCreateEditVM.ContractDuration;
            contract.AccountNumber = contractCreateEditVM.AccountNumber;
            contract.PaymentTerms = contractCreateEditVM.PaymentTerms;
            contract.IsRenewable = contractCreateEditVM.IsRenewable;
            contract.Users = contractCreateEditVM.Users;
            contract.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/contract/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] ContractCreateEditVM contractCreateEditVM)
        {
            DatabaseContext database = new();
            Contract? contract = await database.Contracts.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (contract == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }
            contract.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
