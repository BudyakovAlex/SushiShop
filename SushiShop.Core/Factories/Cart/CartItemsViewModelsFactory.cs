using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Core.ViewModels.Cart.Items.Abstract;
using System;

namespace SushiShop.Core.Factories.Cart
{
    public class CartItemsViewModelsFactory : ICartItemsViewModelsFactory
    {
        public BaseCartProductItemViewModel ProduceProductItemViewModel(
            ICartManager cartManager,
            CartProduct cartProduct,
            Currency? currency,
            string? city,
            Action<int, long>? refreshCountStateAction)
        {
            return cartProduct.Type switch
            {
                ProductType.Item => new CartProductItemViewModel(cartManager, cartProduct, currency, city, null),
                ProductType.Pack => new CartPackItemViewModel(cartManager, cartProduct, currency, city, null),
                ProductType.Sauce => new CartToppingItemViewModel(cartManager, cartProduct, currency, city, refreshCountStateAction),
                _ => throw new NotSupportedException($"ProductType {cartProduct.Type} currently not supported")
            };
        }
    }
}