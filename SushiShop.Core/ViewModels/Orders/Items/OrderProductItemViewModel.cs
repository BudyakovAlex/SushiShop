using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Cart;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Extensions;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderProductItemViewModel : BaseViewModel
    {
        private readonly CartProduct product;
        private readonly Currency currency;

        public OrderProductItemViewModel(CartProduct product, Currency currency)
        {
            this.product = product;
            this.currency = currency;
        }

        public string? Title => product.PageTitle;

        public string Price => $"{product!.TotalPrice} {currency?.Symbol}";

        public string? OldPrice => product.OldPrice == 0 ? null : $"{product!.OldPrice} {currency?.Symbol}";

        public string? ImageUrl => product.ImageInfo?.JpgUrl;

        public string Value => product.GetValue();

        public int CountInBasket => product.Count;
    }
}