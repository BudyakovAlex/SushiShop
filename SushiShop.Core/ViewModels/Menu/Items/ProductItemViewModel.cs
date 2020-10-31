using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.ProductDetails;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class ProductItemViewModel : BaseViewModel
    {
        private readonly Product product;

        public ProductItemViewModel(Product product)
        {
            this.product = product;

            StepperViewModel = new StepperViewModel(0, _ => { });
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public StepperViewModel StepperViewModel { get; }
        public string ImageUrl => product.MainImageInfo?.JpgUrl ?? string.Empty;
        public string Title => product.PageTitle;
        public string Price => $"{product.Price} {product.Currency.Symbol}";
        public string? OldPrice => product.OldPrice == 0 ? null : $"{product.OldPrice} {product.Currency.Symbol}";

        private Task ShowDetailsAsync() => NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters>(new CardProductNavigationParameters(product.Id, null));
    }
}
