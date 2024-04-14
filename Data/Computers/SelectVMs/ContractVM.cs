using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data.Computers.SelectVMs
{
    public class ContractVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("contractype")]
        public string? ContractType { get; set; }
        [JsonPropertyName("startdate")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("contractduration")]
        public string? ContractDuration { get; set; }
        [JsonPropertyName("accountnumber")]
        public string? AccountNumber { get; set; }
        [JsonPropertyName ("paymentterms")]
        public string? PaymentTerms { get; set; }
        [JsonPropertyName("isrenewable")]
        public bool? IsRenewable { get; set; }
        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
}
