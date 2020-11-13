using System;
using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class GetProductDto
    {
        [JsonProperty("basketId")]
        public Guid BaseketId { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }
    }
}