using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class NetworkDeviceVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? DeviceType { get; set; }
        public string? Rack { get; set; }
        public string? SerialNumber { get; set; }
        public string? InventoryNumber { get; set; }
        public string? Users { get; set; }
    }
}
