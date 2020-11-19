using Newtonsoft.Json;
using System;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class GetToppingDto
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
