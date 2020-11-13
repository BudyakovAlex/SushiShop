using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class PromoCodeDto
    {
        [JsonProperty("promocode")]
        public string PromoCode { get; set; }

        [JsonProperty("discountFixed")]
        public long DiscountFixed { get; set; }

        [JsonProperty("discountPercent")]
        public int? DiscountPercent { get; set; }

        [JsonProperty("itemGift")]
        public string? ItemGift { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
