using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public class UserDevicesModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("user")]
        public string User { get; set; }
    }
}
