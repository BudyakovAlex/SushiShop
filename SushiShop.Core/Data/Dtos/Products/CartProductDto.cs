using System;
using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class CartProductDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("pagetitle")]
        public string? Pagetitle { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("toppings")]
        public GetToppingDto[] Toppings { get; set; } = Array.Empty<GetToppingDto>();

    }
}
