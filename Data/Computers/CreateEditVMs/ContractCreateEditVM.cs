using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class ContractCreateEditVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StatusId { get; set; }
        public int? LocationId { get; set; }
        public int? ContractType { get; set; }
        public DateTime? StartDate { get; set; }
        public int? ContractDuration { get; set; }
        public string? AccountNumber { get; set; }
        public int? PaymentTerms { get; set; }
        public bool? IsRenewable { get; set; }
        public int? Users { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
