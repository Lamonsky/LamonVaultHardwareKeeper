using System.Text.Json.Serialization;

namespace Data.Helpers
{
    public class MainWindowModel
    {
        [JsonPropertyName("computerCount")]
        public int ComputerCount { get; set; }

        [JsonPropertyName("monitorCount")]
        public int MonitorCount { get; set; }

        [JsonPropertyName("softwareCount")]
        public int SoftwareCount { get; set; }

        [JsonPropertyName("licenseCount")]
        public int LicenseCount { get; set; }

        [JsonPropertyName("networkDeviceCount")]
        public int NetworkDeviceCount { get; set; }

        [JsonPropertyName("deviceCount")]
        public int DeviceCount { get; set; }

        [JsonPropertyName("printerCount")]
        public int PrinterCount { get; set; }

        [JsonPropertyName("phoneCount")]
        public int PhoneCount { get; set; }

        [JsonPropertyName("rackCabinetCount")]
        public int RackCabinetCount { get; set; }

        [JsonPropertyName("hardDriveCount")]
        public int HardDriveCount { get; set; }

        [JsonPropertyName("serverCount")]
        public int ServerCount { get; set; }

        [JsonPropertyName("simCardCount")]
        public int SimCardCount { get; set; }

        [JsonPropertyName("usersCount")]
        public int UsersCount { get; set; }

        [JsonPropertyName("ticketsCount")]
        public int TicketsCount { get; set; }
        [JsonPropertyName("locationCount")]
        public int LocationCount { get; set; }


    }
}
