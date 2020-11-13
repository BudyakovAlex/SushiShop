using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Dtos.Products;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Resources;

namespace SushiShop.Core.ViewModels.Cart
{
    public class CartViewModel : BasePageViewModel
    {
        private readonly ICartManager cartManager;

        private Data.Models.Cart.Cart? cart;

        private int id;
        private string? city;

        public CartViewModel(ICartManager cartManager)
        {
            this.cartManager = cartManager;

            CheckoutCommand = new SafeAsyncCommand(ExecutionStateWrapper, CheckoutAsync);
            AddSauceCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddSauceAsync);
        }

        public IMvxCommand AddSauceCommand { get; }
        //public IMvxCommand AddBagCommand { get; }
        public IMvxCommand CheckoutCommand { get; }

        public string Title => AppStrings.Basket;
        public MvxObservableCollection<CartProductDto> Products { get; }
        public string ProductUrl => string.Empty;
        public string ProductName => string.Empty;
        public int CountProductsInCart => cart.Products.Length;
        public string TotalPrice => cart.TotalSum.ToString();
        
        //public override void Prepare(CardProductNavigationParameters parameter)
        //{
        //    id = parameter.Id;
        //    city = parameter.City;
        //}

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getBasket = cartManager.GetProductInCartAsync(id, city);

            await Task.WhenAll(getBasket);

            cart = getBasket.Result.Data;
            //var relatedProducts = cart.Products.
            //toppings = product?.Params?.AvailableToppings?.ToList() ?? new List<Topping>();

            //var viewModels = relatedProducts.Select(product => new ProductItemViewModel(cartManager, product, city)).ToList();
            //RelatedItems.AddRange(viewModels);
            await RaiseAllPropertiesChanged();
        }
        private Task AddSauceAsync()
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