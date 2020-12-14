using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Orders
{
    public class PickupPointDto
    {
        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("phones")]
        public string[]? Phones { get; set; }

        [JsonProperty("worktimeState")]
        public string? WorktimeState { get; set; }

        [JsonProperty("worktime")]
        public string[]? Worktime { get; set; }

        [JsonProperty("cordinates")]
        public CoordinatesDto? Coordinates { get; set; }
    }
}