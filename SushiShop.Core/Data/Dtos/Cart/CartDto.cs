using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Dtos.Products;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class CartDto
    {
        [JsonProperty("basketId")] 
        public int BasketId { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("priceGroup")]
        public string? PriceGroup { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("totalSum")]
        public long TotalSum { get; set; }

        [JsonProperty("discount")]
        public CurrencyDto? Currency { get; set; }

        [JsonProperty("products")]
        public CartProductDto? Products { get; set; }

        [JsonProperty("promocode")]
        public PromocodeDto? PromoCode { get; set; }

        [JsonProperty("productUid")]
        public Guid ProductUid { get; set; }
    }
}
