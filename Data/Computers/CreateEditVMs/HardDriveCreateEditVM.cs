using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class HardDriveCreateEditVM
    {
        public int Id { get; set; }

        public int? Manufacturer { get; set; }

        public int? Model { get; set; }

        public int? Capacity { get; set; }

        public int? Server { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }
        public int? Status { get; set; }
    }
}
