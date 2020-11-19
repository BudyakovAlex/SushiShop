using System;
using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class CartProductDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("amount")]
        public decimal? TotalPrice { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
        
        [JsonProperty("readonly")]
        public bool? IsReadonly { get; set; }

        [JsonProperty("toppings")]
        public CartToppingDto[] Toppings { get; set; } = Array.Empty<CartToppingDto>();

    }
}
