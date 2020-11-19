using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class CartToppingDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }
    }
}
