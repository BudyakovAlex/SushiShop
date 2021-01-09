using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Shops
{
    public class MetroShopDto : MetroDto
    {
        public ShopDto? Shop { get; set; }
    }
}