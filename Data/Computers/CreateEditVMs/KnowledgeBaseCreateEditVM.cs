using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class KnowledgeBaseCreateEditVM
    {
        public int Id { get; set; }

        public int? Category { get; set; }

        public string? Subject { get; set; }

        public string? Content { get; set; }

        public int? Users { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }
        public int? Status { get; set; }
    }
}
