using System;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

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

        [JsonProperty("oldPrice")]
        public decimal OldPrice { get; set; }

        [JsonProperty("amount")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("weight")]
        public decimal? Weight { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
        
        [JsonProperty("readonly")]
        public bool IsReadOnly { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("toppings")]
        public CartToppingDto[]? Toppings { get; set; }

        [JsonProperty("image")]
        public ImageInfoDto? ImageInfo { get; set; }
    }
}