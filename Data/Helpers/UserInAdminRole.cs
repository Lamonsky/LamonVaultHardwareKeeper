using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public class UserInAdminRole
    {
        [JsonPropertyName("isInRole")]
        public bool IsInRole { get; set; }
    }
}
