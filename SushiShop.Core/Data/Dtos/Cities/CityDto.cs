using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Cities
{
    public class CityDto
    {
        public long Id { get; set; }
        public string? City { get; set; }
        [JsonProperty("shops_count")]
        public int ShopsCount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonProperty("Timezone")]
        public string? TimeZone { get; set; }
        public string? PriceGroup { get; set; }
        public string? Phone { get; set; }
        public string? CityGroups { get; set; }
        [JsonProperty("haveMetro")]
        public bool IsMetroAvailable { get; set; }
        public CurrencyDto? Currency { get; set; }
    }
}
