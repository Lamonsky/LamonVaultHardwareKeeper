using Data.Computers.CreateEditVMs;
using Data.Computers.SelectVMs;
using DatabaseRestApi.Models.Contexts;
using DatabaseRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Controllers
{
    public class PrinterController : Controller
    {
        [Route("/printer")]
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
                    SerialNumber = item.SerialNumber,
                    InventoryNumber = item.InventoryNumber,
                    Users = item.UsersNavigation.FirstName + " " + item.UsersNavigation.LastName
                }).ToListAsync();
            return Json(PrintersVM);
        }

        [Route("/printer")]
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
                InventoryNumber = printerCreateEditVM.InventoryNumber,
                Users = printerCreateEditVM.Users,
                CreatedAt = DateTime.Now
            });

            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/printer/{id}")]
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
            printer.Users = printerCreateEditVM.Users;
            printer.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }

        [Route("/printer/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromBody] PrintersCreateEditVM printerCreateEditVM)
        {
            DatabaseContext database = new();
            Printer printer = await database.Printers.Where(item => item.Id == id && item.StatusId != 99).FirstOrDefaultAsync();
            if (printer == null)
            {
                return BadRequest("Nie ma komputera o podanym id {id}");
            }

            printer.ModifiedAt = DateTime.Now;
            await database.SaveChangesAsync();
            return Ok();
        }
    }
}
