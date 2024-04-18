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
            MainWindowModel mainWindowModel = new MainWindowModel(0,0,0,0);
            mainWindowModel.ComputerCount = database.Computers.Where(item => item.StatusId != 99 ).Count();
            mainWindowModel.MonitorCount = database.Monitors.Where(item => item.StatusId != 99).Count();
            mainWindowModel.UserCount = database.Users.Where(item => item.IsActive == true).Count();
            mainWindowModel.LocationCount = database.Locations.Where(item => item.Status != 99).Count();
            return Json(mainWindowModel);
        }
    }
}
