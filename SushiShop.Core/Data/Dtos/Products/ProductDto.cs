using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using System;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class ProductDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }
        public string? Alias { get; set; }
        public long Parent { get; set; }
        [JsonProperty("pub_date")]
        public long PublishDate { get; set; }
        [JsonProperty("unpub_date")]
        public long UnpublishDate { get; set; }
        [JsonProperty("introtext")]
        public string? IntroText { get; set; }
        [JsonProperty("createdon")]
        public long CreationDate { get; set; }
        [JsonProperty("params")]
        public ProductParametersDto? Params { get; set; }
        public string? PriceGroup { get; set; }
        public CurrencyDto? Currency { get; set; }
        public long Price { get; set; }
        public long OldPrice { get; set; }
        [JsonProperty("itemImage")]
        public ImageInfoDto? MainImageInfo { get; set; }
        [JsonProperty("itemImage2")]
        public ImageInfoDto? OptionalImageInfo { get; set; }
        public long CountInBasket { get; set; }
    }
}
