using Newtonsoft.Json;
using System;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class UpdateProductDto
    {
        [JsonProperty("basketId")]
        public Guid BasketId { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("toppings")]
        public UpdateToppingDto[] Toppings { get; set; } = Array.Empty<UpdateToppingDto>();
    }
}