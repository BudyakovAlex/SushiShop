using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class CartDto
    {
        [JsonProperty("basketId")] 
        public Guid BasketId { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("priceGroup")]
        public string? PriceGroup { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("totalSum")]
        public decimal TotalSum { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }        

        [JsonProperty("currency")]
        public CurrencyDto? Currency { get; set; }

        [JsonProperty("products")]
        public CartProductDto[]? Products { get; set; }

        [JsonProperty("promocode")]
        public PromocodeDto? Promoсode { get; set; }
    }
}