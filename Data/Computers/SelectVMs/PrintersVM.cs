﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data.Computers.SelectVMs
{
    public class PrintersVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("printertype")]
        public string? PrinterType { get; set; }
        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }
        [JsonPropertyName("model")]
        public string? Model { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("ipaddress")]
        public string? IpAddress { get; set; }
        [JsonPropertyName("users")]
        public string? Users { get; set; }
    }
}
