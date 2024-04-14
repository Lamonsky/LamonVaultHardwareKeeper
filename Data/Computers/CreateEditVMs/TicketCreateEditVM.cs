using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class TicketCreateEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? Type { get; set; }

        public int? Category { get; set; }

        public int? StatusId { get; set; }

        public int? LocationId { get; set; }

        public int? User { get; set; }

        public int? Owner { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
