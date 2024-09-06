using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class TicketStatusCreateEditVM : DictionaryCreateEditVM
    {
        [JsonPropertyName("countToClosed")]
        public bool CountToClosed { get; set; }
    }
}
