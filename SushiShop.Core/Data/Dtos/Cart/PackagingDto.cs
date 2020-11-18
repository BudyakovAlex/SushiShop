using System;
using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class PackagingDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("alias")]
        public string? Alias { get; set; }

        [JsonProperty("parent")]
        public long Parent { get; set; }

        [JsonProperty("pub_date")]
        public DateTime PublishDate { get; set; }

        [JsonProperty("unpub_date")]
        public DateTime UnpublishDate { get; set; }

        [JsonProperty("introtext")]
        public string? IntroText { get; set; }

        [JsonProperty("createdon")]
        public DateTime CreationDate  { get; set; }

        [JsonProperty("priceGroup")]
        public string? PriceGroup { get; set; }

        [JsonProperty("countInBasket")]
        public int CountInBasket { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("oldPrice")]
        public long OldPrice { get; set; }

        [JsonProperty("currency")]
        public CurrencyDto? Currency { get; set; }

        [JsonProperty("itemImage")]
        public ImageInfoDto? MainImageInfo { get; set; }

        [JsonProperty("itemImage2")]
        public ImageInfoDto? OptionalImageInfo { get; set; }
    }
}
