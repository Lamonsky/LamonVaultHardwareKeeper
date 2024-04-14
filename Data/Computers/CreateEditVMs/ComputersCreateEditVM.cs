using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class ComputersCreateEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? LocationId { get; set; }
        public int? StatusId { get; set; }
        public int? ComputerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ComputerModelId { get; set; }
        public string? Processor { get; set; }
        public string? Ram { get; set; }
        public string? Disk { get; set; }
        public string? GraphicsCard { get; set; }
        public int? OperatingSystemId { get; set; }
        public string? SerialNumber { get; set; }
        public string? InventoryNumber { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedByNavigation { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedByNavigation { get; set; }
    }
}
