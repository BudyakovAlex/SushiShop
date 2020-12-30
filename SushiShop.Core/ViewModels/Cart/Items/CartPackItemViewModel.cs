using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Cart.Items.Abstract;
using System;

namespace SushiShop.Core.ViewModels.Cart.Items
{
    public class CartPackItemViewModel : BaseCartProductItemViewModel
    {
        public CartPackItemViewModel(
            ICartManager cartManager,
            CartProduct product,
            Currency? currency,
            string? city,
            Action<int, long>? refreshCountStateAction) : base(cartManager, product, currency, city, refreshCountStateAction)
        {
        }
    }
}