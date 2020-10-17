using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Dtos.City
{
    public class CityDto
    {
        public int Id { get; set; }
        public string? City { get; set; }
        [JsonProperty("shops_count")]
        public int ShopsCount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonProperty("Timezone")]
        public string? TimeZone { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string? Phone { get; set; }
        public string? CityGroups { get; set; }
        public CurrencyDto? Currency { get; set; }
    }
}
