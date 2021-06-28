using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Dtos.Products;

namespace SushiShop.Core.Data.Dtos.Promotions
{
    public class PromotionDto
    {
        public long Id { get; set; }

        public string? PageTitle { get; set; }

        public string? LongTitle { get; set; }

        public string? Alias { get; set; }

        public string? IntroText { get; set; }

        public string? Content { get; set; }

        [JsonProperty("pub_date")]
        public int PubDate { get; set; }

        [JsonProperty("unpub_date")]
        public int UnpubDate { get; set; }

        public int CreatedOn { get; set; }

        public string? Url { get; set; }

        [JsonProperty("saleShowOnHome")]
        public bool? ShouldShowOnHome { get; set; }

        public ImageInfoDto? SaleSquareImage { get; set; }

        public ImageInfoDto? SaleRectangularImage { get; set; }

        public string[]? CityMulti { get; set; }

        public ProductDto? SaleProduct { get; set; }
    }
}
