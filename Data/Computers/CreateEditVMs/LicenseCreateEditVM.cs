using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class LicenseCreateEditVM
    {
        public int Id { get; set; }

        public int? Software { get; set; }

        public string? Name { get; set; }

        public int? LicenseType { get; set; }

        public int? Publisher { get; set; }

        public int? StatusId { get; set; }

        public int? LocationId { get; set; }

        public string? SerialNumber { get; set; }

        public string? InventoryNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? Users { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
