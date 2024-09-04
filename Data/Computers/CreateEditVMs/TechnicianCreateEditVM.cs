﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.CreateEditVMs
{
    public class TechnicianCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("users_id")]
        public int? UsersId { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdby")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("modifiedat")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedby")]
        public int? ModifiedBy { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }
}
