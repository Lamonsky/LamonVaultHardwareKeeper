using System.Text.Json.Serialization;

namespace Data.Helpers
{
    public class MainWindowModel
    {
        [JsonPropertyName("computerCount")]
        public int ComputerCount {  get; set; }
        [JsonPropertyName("monitorCount")]
        public int MonitorCount { get; set; }
        [JsonPropertyName("userCount")]
        public int UserCount { get; set; }
        [JsonPropertyName("locationCount")]
        public int LocationCount { get; set; }
        public MainWindowModel(int computerCount, int monitorCount, int userCount, int locationCount)
        {
            ComputerCount = computerCount;
            MonitorCount = monitorCount;
            UserCount = userCount;
            LocationCount = locationCount;
        }
    }
}
