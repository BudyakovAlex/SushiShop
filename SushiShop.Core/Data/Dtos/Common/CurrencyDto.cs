using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class CurrencyDto
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("decimals")]
        public int Decimals { get; set; }
    }
}