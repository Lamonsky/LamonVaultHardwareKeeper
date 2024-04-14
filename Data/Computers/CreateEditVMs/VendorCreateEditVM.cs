using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class VendorCreateEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? AdministrativeNumber { get; set; }

        public string? Phone { get; set; }

        public string? Fax { get; set; }

        public string? Website { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public bool? IsActive { get; set; }

        public int? Users { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
