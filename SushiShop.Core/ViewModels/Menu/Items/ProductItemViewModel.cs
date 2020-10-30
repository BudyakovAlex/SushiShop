using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.ViewModels.Common;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class ProductItemViewModel : BaseViewModel
    {
        private readonly Product product;

        public ProductItemViewModel(Product product)
        {
            this.product = product;

            StepperViewModel = new StepperViewModel(0, _ => { });
        }

        public StepperViewModel StepperViewModel { get; }

        public string ImageUrl => product.MainImageInfo?.JpgUrl ?? string.Empty;
        public string Title => product.PageTitle;
        public string Price => $"{product.Price} {product.Currency.Symbol}";
        public string? OldPrice => product.OldPrice == 0 ? null : $"{product.OldPrice} {product.Currency.Symbol}";
    }
}
