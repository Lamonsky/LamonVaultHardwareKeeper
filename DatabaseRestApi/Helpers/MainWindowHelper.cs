using Data;
using Data.Helpers;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseRestApi.Helpers
{
    public class MainWindowHelper : Controller
    {
        [Route(URLs.MAINWINDOW)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DatabaseContext database = new DatabaseContext();
            MainWindowModel mainWindowModel = new MainWindowModel();
            mainWindowModel.ComputerCount = database.Computers.Where(item => item.StatusId != 99 ).Count();
            mainWindowModel.MonitorCount = database.Monitors.Where(item => item.StatusId != 99).Count();
            mainWindowModel.SoftwareCount = database.Softwares.Where(item => item.Status != 99).Count();
            mainWindowModel.LicenseCount = database.Licenses.Where(item => item.StatusId != 99).Count();
            mainWindowModel.NetworkDeviceCount = database.NetworkDevices.Where(item => item.StatusId != 99 ).Count();
            mainWindowModel.DeviceCount= database.Devices.Where(item => item.StatusId != 99).Count();
            mainWindowModel.PrinterCount = database.Printers.Where(item => item.StatusId != 99).Count();
            mainWindowModel.PhoneCount = database.Phones.Where(item => item.StatusId != 99).Count();
            mainWindowModel.RackCabinetCount = database.RackCabinets.Where(item => item.StatusId != 99).Count();
            mainWindowModel.HardDriveCount = database.HardDrives.Where(item => item.Status != 99).Count();
            mainWindowModel.ServerCount = database.Servers.Where(item => item.StatusId != 99 ).Count();
            mainWindowModel.SimCardCount = database.SimCards.Where(item => item.StatusId != 99).Count();
            mainWindowModel.UsersCount = database.Users.Where(item => item.IsActive == true).Count();
            mainWindowModel.TicketsCount = database.Tickets.Where(item => item.Status.CountToClosed == false).Where(item => item.StatusId != 99).Count();
            mainWindowModel.LocationCount = database.Locations.Where(item => item.Status != 99).Count();
            return Json(mainWindowModel);
        }
        [Route(URLs.MAINWINDOW_ID)]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            DatabaseContext database = new DatabaseContext();
            MainWindowModel mainWindowModel = new MainWindowModel();
            mainWindowModel.ComputerCount = database.Computers.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.MonitorCount = database.Monitors.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.SoftwareCount = database.Softwares.Where(item => item.Status != 99).Where(item => item.Users == id).Count();
            mainWindowModel.LicenseCount = database.Licenses.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.NetworkDeviceCount = database.NetworkDevices.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.DeviceCount= database.Devices.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.PrinterCount = database.Printers.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.PhoneCount = database.Phones.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.RackCabinetCount = database.RackCabinets.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.ServerCount = database.Servers.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.SimCardCount = database.SimCards.Where(item => item.StatusId != 99).Where(item => item.Users == id).Count();
            mainWindowModel.TicketsCount = database.Tickets.Where(item => item.Status.CountToClosed == false).Where(item => item.StatusId != 99).Where(item => item.User == id).Count();
            return Json(mainWindowModel);
        }
    }
}
