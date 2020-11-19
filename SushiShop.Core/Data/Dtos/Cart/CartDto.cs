using System;
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
        public int TotalSum { get; set; }

        [JsonProperty("discount")]
        public long Discount { get; set; }        

        [JsonProperty("currency")]
        public CurrencyDto[]? Currency { get; set; }

        [JsonProperty("products")]
        public CartProductDto[]? Products { get; set; }

        [JsonProperty("promocode")]
        public PromocodeDto[]? Promoсodes { get; set; }
    }
}
