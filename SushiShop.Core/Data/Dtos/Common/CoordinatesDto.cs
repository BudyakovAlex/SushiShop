using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class CoordinatesDto
    {
        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }
    }
}