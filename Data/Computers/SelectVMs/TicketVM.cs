using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class TicketVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? Location { get; set; }

        public string? User { get; set; }

        public string? Owner { get; set; }
    }
}
