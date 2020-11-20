using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Factories.Cart;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Core.ViewModels.ProductDetails;

namespace SushiShop.Core.ViewModels.Cart
{
    public class CartViewModel : BasePageViewModel
    {
        private readonly ICartManager cartManager;
        private readonly IUserSession userSession;
        private readonly ICartItemsViewModelsFactory viewModelsFactory;

        private Topping[]? sauses;
        private Data.Models.Cart.Cart? cart;

        private string? city;

        public CartViewModel(
            ICartManager cartManager,
            IUserSession userSession,
            ICartItemsViewModelsFactory viewModelsFactory)
        {
            this.cartManager = cartManager;
            this.userSession = userSession;
            this.viewModelsFactory = viewModelsFactory;
            Products = new MvxObservableCollection<CartProductItemViewModel>();
            Sauces = new MvxObservableCollection<CartToppingItemViewModel>();
            Packages = new MvxObservableCollection<CartPackItemViewModel>();

            CheckoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, CheckoutAsync);
            AddSauceCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddSauceAsync);
        }

        public IMvxCommand AddSauceCommand { get; }
        public IMvxCommand CheckoutCommand { get; }

        public MvxObservableCollection<CartProductItemViewModel> Products { get; }

        public MvxObservableCollection<CartToppingItemViewModel> Sauces { get; }

        public MvxObservableCollection<CartPackItemViewModel> Packages { get; }

        public string Title => AppStrings.Basket;

        public long? CountProductsInCart => cart?.TotalCount;

        public decimal? TotalPrice => cart?.TotalSum;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _ = ExecutionStateWrapper.WrapAsync(() => ReloadDataAsync(), awaitWhenBusy: true);  
        }

        private async Task ReloadDataAsync()
        {
            city = userSession.GetCity()?.Name;

            var getBasket = cartManager.GetCartAsync(city);
            var packagingCart = cartManager.GetCartPackagingAsync(city);
            var getSauces = cartManager.GetSaucesAsync(city);

            await Task.WhenAll(getBasket, packagingCart, getSauces);

            sauses = getSauces.Result.Data;
            cart = getBasket.Result.Data;

            var allProducts = cart!.Products.Select(product => viewModelsFactory.ProduceProductItemViewModel(cartManager, product, cart.Currency, cart.City)).ToArray();

            var mainProducts = allProducts.OfType<CartProductItemViewModel>().ToList();
            var toppings = allProducts.OfType<CartToppingItemViewModel>().ToList();
            var packages = allProducts.OfType<CartPackItemViewModel>().ToList();

            Products.ReplaceWith(mainProducts);
            Sauces.ReplaceWith(toppings);
            Packages.ReplaceWith(packages);

            await Task.WhenAll(RaisePropertyChanged(nameof(CountProductsInCart)),
                RaisePropertyChanged(nameof(TotalPrice)));
        }

        private async Task AddSauceAsync()
        {
            var navigationParams = new ToppingNavigationParameters(sauses.ToList(), AppStrings.AddSauce);
            var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
            if (result is null)
            {
                return;
            }

            //TODO: add send logic here
        }

        private Task CheckoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}