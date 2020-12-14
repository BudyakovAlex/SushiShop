using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.NavigationParameters
{
    public class OrderCompositionNavigationParameters
    {
        public OrderCompositionNavigationParameters(CartProduct[]? products, Currency? currency)
        {
            Products = products;
            Currency = currency;
        }

        public CartProduct[]? Products { get; }

        public Currency? Currency { get; }
    }
}