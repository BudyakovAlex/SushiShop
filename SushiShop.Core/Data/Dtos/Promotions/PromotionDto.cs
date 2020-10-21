using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Promotions
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string? PageTitle { get; set; }
        public string? LongTitle { get; set; }
        public string? Alias { get; set; }
        public string? IntroText { get; set; }
        public string? Content { get; set; }
        public int PubDate { get; set; }
        public int UnpubDate { get; set; }
        public int CreatedOn { get; set; }
        public string? Url { get; set; }
        public string? SaleShowOnHome { get; set; }
        public ImageInfoDto? SaleSquareImage { get; set; }
        public ImageInfoDto? SaleRectangularImage { get; set; }
        public string[]? CityMulti { get; set; }
        public object? SaleProductId { get; set; }
    }
}
