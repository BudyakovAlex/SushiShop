using System;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Products;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class CartProductDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
        
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
        
        [JsonProperty("readonly")]
        public bool? ReadonlyFlag { get; set; }

        [JsonProperty("toppings")]
        public GetToppingDto[] Toppings { get; set; } = Array.Empty<GetToppingDto>();

    }
}
