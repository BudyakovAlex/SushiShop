using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using System;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class ProductDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("alias")]
        public string? Alias { get; set; }

        [JsonProperty("parent")]
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

        [JsonProperty("countInBasket")]
        public int CountInBasket { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("oldPrice")]
        public long OldPrice { get; set; }

        [JsonProperty("currency")]
        public CurrencyDto? Currency { get; set; }

        [JsonProperty("itemImage")]
        public ImageInfoUriDto? MainImageInfo { get; set; }

        [JsonProperty("itemImage2")]
        public ImageInfoUriDto? OptionalImageInfo { get; set; }
    }
}