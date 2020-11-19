using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cart.Items;

namespace SushiShop.Core.ViewModels.Cart
{
    public class CartViewModel : BasePageViewModel
    {
        private readonly ICartManager cartManager;
        private readonly IUserSession userSession;

        private Data.Models.Cart.Cart? cart;

        private int id;
        private string? city;

        public CartViewModel(ICartManager cartManager, IUserSession userSession)
        {
            this.cartManager = cartManager;
            this.userSession = userSession;

            Products = new MvxObservableCollection<CartProductItemViewModel>();
            Sauces = new MvxObservableCollection<CartToppingtItemViewModel>();
            Packagings = new MvxObservableCollection<CartPackItemViewModel>();

            CheckoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, CheckoutAsync);
            AddSauceCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddSauceAsync);
        }

        public IMvxCommand AddSauceCommand { get; }
        public IMvxCommand CheckoutCommand { get; }

        public MvxObservableCollection<CartProductItemViewModel> Products { get; }
        public MvxObservableCollection<CartToppingtItemViewModel> Sauces { get; }
        public MvxObservableCollection<CartPackItemViewModel> Packagings { get; }

        public string Title => AppStrings.Basket;
        public long? CountProductsInCart => cart?.TotalCount;
        public int? TotalPrice => cart?.TotalSum;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            city = userSession.GetCity()?.Name;

            var getBasket = cartManager.GetCartAsync(id, city);
            var packagingCart = cartManager.GetCartPackagingAsync(id, city);

            await Task.WhenAll(getBasket, packagingCart);

            cart = getBasket.Result.Data;
            Products.AddRange(cart.Products.Select(product => new CartProductItemViewModel(cartManager, product, city)).ToList());

            //Packagings = new MvxObservableCollection<CartProductItemViewModel> packagingCart.Result.Data.ToList()
            //var packViewModels = packagingList.Select(packaging => new ProductItemViewModel(cartManager, packaging, city)).ToList();

            //var viewModels = relatedProducts.Select(product => new ProductItemViewModel(cartManager, product, city)).ToList();
            //RelatedItems.AddRange(viewModels);
            await RaiseAllPropertiesChanged();
        }

        private async Task AddSauceAsync()
        {
            //var navigationParams = new ToppingNavigationParameters(toppings, AppStrings.MakeItTastier);
            //var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
            //if (result is null)
            //{
            //    return;
            //}
        }

        private Task CheckoutAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}