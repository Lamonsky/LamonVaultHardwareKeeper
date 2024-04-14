using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class PrintersCreateEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LocationId { get; set; }
        public int? StatusId { get; set; }
        public int? PrinterType { get; set; }
        public int? Manufacturer { get; set; }
        public int? Model { get; set; }
        public string? SerialNumber { get; set; }
        public string? InventoryNumber { get; set; }
        public string? IpAddress { get; set; }
        public int? Users { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
