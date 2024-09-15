using Data;
using Data.Computers.SelectVMs;
using Data.Helpers;
using DatabaseRestApi.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRestApi.Helpers
{
    public class UserDevices : Controller
    {
        [Authorize]
        [HttpGet]
        [Route(URLs.USER_DEVICES)]
        public async Task<IActionResult> Index(int id)
        {
            List<UserDevicesModel> userDevices = new List<UserDevicesModel>();
            DatabaseContext database = new();
            List<UserDevicesModel> computers = await database.Computers
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Komputer: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in computers)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> devices = await database.Devices
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Urządzenie: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in devices)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> licenses = await database.Licenses
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Licencja: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in licenses)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> monitors = await database.Monitors
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Monitor: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in monitors)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> nwd = await database.NetworkDevices
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Urządzenie sieciowe: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in nwd)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> phone = await database.Phones
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Telefon: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in phone)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> printer = await database.Printers
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Drukarka: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in printer)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> rc = await database.RackCabinets
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Szafa Rack: {item.Manufacturer}, {item.Model}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in rc)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> ser = await database.Servers
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Serwer: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in ser)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }
            List<UserDevicesModel> sim = await database.SimCards
                .Where(item => item.Users == id)
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Karta SIM:{item.PhoneNumber}, {item.SerialNumber}, {item.InventoryNumber}"
                }).ToListAsync();

            foreach(UserDevicesModel item in sim)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name
                }) ;
            }

            return Json(userDevices);
        }
        
        [Authorize]
        [HttpGet]
        [Route(URLs.USER_DEVICES_ALL)]
        public async Task<IActionResult> GetAll(int id)
        {
            List<UserDevicesModel> userDevices = new List<UserDevicesModel>();
            DatabaseContext database = new();
            List<UserDevicesModel> computers = await database.Computers
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Komputer: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in computers)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> devices = await database.Devices
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Urządzenie: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in devices)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> licenses = await database.Licenses
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Licencja: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in licenses)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> monitors = await database.Monitors
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Monitor: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in monitors)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> nwd = await database.NetworkDevices
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Urządzenie sieciowe: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in nwd)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> phone = await database.Phones
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Telefon: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in phone)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> printer = await database.Printers
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Drukarka: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in printer)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> rc = await database.RackCabinets
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Szafa Rack: {item.Manufacturer}, {item.Model}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in rc)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> ser = await database.Servers
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Serwer: {item.Name}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();
            foreach(UserDevicesModel item in ser)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }
            List<UserDevicesModel> sim = await database.SimCards
                
                .Where(item => item.StatusId != 99)
                .Select(item => new UserDevicesModel()
                {
                    Name = $"Karta SIM:{item.PhoneNumber}, {item.SerialNumber}, {item.InventoryNumber}",
                    User = $"{item.UsersNavigation.Usersname} {item.UsersNavigation.FirstName} {item.UsersNavigation.LastName} {item.UsersNavigation.InternalNumber}"
                }).ToListAsync();

            foreach(UserDevicesModel item in sim)
            {
                userDevices.Add(new UserDevicesModel
                {
                    Name = item.Name,
                    User = item.User
                }) ;
            }

            return Json(userDevices);
        }
    }
}
