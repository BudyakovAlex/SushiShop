using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class MetroDto
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("line")]
        public string? Line { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("measurement")]
        public string? Measurement { get; set; }
    }
}