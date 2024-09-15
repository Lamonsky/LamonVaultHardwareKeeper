using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class UsersWithRolesVM
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
        [JsonPropertyName("fixedroles")]
        public string Fixedroles { get; set; }
    }
}
