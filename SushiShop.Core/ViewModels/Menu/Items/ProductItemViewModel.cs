using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.ProductDetails;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class ProductItemViewModel : BaseViewModel
    {
        private readonly ICartManager cartManager;

        private readonly Product product;
        private readonly string? city;
        private readonly Func<Task>? refreshDataFunc;

        public ProductItemViewModel(
            ICartManager cartManager,
            Product product,
            string? city,
            Func<Task>? refreshDataFunc)
        {
            this.cartManager = cartManager;
            this.product = product;
            this.city = city;
            this.refreshDataFunc = refreshDataFunc;

            StepperViewModel = new StepperViewModel(product.CountInBasket, OnCountChangedAsync);
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public StepperViewModel StepperViewModel { get; }

        public StickerParams[]? Stickers => product.Params?.Stickers;

        public string? ImageUrl => product.MainImageInfo?.JpgUrl;

        public string Title => product.PageTitle;

        public string Price => $"{product.Price} {product.Currency.Symbol}";

        public string? OldPrice => product.OldPrice == 0 ? null : $"{product.OldPrice} {product.Currency.Symbol}";

        internal long ParentId => product.Parent;

        private async Task ShowDetailsAsync()
        {
            var parameters = new CardProductNavigationParameters(product.Id, null);
            var shouldRefresh = await NavigationManager.NavigateAsync<ProductDetailsViewModel, CardProductNavigationParameters, bool>(parameters);
            if (!shouldRefresh)
            {
                return;
            }

            var refreshTask = refreshDataFunc?.Invoke() ?? Task.CompletedTask;
            await refreshTask;
        }

        private async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            var step = newCount - previousCount;
            var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, step, Array.Empty<Topping>());
            if (response.Data is null)
            {
                StepperViewModel.Count = previousCount;
                return;
            }

            Messenger.Publish(new RefreshCartMessage(this));
            product!.Uid = response.Data.Uid;
        }
    }
}