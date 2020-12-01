using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.ViewModels.Cart.Items.Abstract;

namespace SushiShop.Core.Factories.Cart
{
    public interface ICartItemsViewModelsFactory
    {
        BaseCartProductItemViewModel ProduceProductItemViewModel(
            ICartManager cartManager,
            CartProduct cartProduct,
            Currency? currency,
            string? city);
    }
}