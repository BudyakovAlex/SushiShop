using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class GetToppingDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("pagetitle")]
        public int PageTitle { get; set; }
    }
}
