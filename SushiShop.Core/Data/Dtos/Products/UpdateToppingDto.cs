using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class UpdateToppingDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
