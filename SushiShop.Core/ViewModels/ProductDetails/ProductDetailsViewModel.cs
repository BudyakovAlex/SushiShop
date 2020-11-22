using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Menu.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel : BasePageViewModel<CardProductNavigationParameters, bool>
    {
        private readonly IProductsManager productsManager;
        private readonly ICartManager cartManager;

        private Product? product;
        private List<Topping> toppings;

        private long id;
        private string? city;
        private bool hasChanged;

        public ProductDetailsViewModel(IProductsManager productsManager, ICartManager cartManager)
        {
            this.productsManager = productsManager;
            this.cartManager = cartManager;

            toppings = new List<Topping>();

            StepperViewModel = new StepperViewModel(0, OnCountChangedAsync);
            RelatedItems = new MvxObservableCollection<ProductItemViewModel>();
            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
        }

        public IMvxCommand AddToCartCommand { get; }

        public bool IsReadOnly { get; private set; }

        public string? BackgroungImageUrl => product?.MainImageInfo?.OriginalUrl;

        public string Protein => product?.Params?.Proteins ?? string.Empty;

        public string Fats => product?.Params?.Fats ?? string.Empty;

        public string Carbohydrates => product?.Params?.Carbons ?? string.Empty;

        public string Calories => product?.Params?.CalorificValue ?? string.Empty;

        public string Title => product?.PageTitle ?? string.Empty;

        public string Description => product?.IntroText ?? string.Empty;

        public string Weight => product?.Params?.Weight ?? string.Empty;

        public string Price => product is null ? string.Empty : $"{product.Price} {product.Currency.Symbol}";

        public string? OldPrice => product is null || product.OldPrice == 0
            ? string.Empty
            : $"{product.OldPrice} {product.Currency.Symbol}";

        public StepperViewModel StepperViewModel { get; }

        public MvxObservableCollection<ProductItemViewModel> RelatedItems { get; }

        private bool isHiddenStepper = true;
        public bool IsHiddenStepper
        {
            get => isHiddenStepper;
            set => SetProperty(ref isHiddenStepper, value);
        }

        protected override bool DefaultResult => hasChanged;

        public override void Prepare(CardProductNavigationParameters parameter)
        {
            id = parameter.Id;
            city = parameter.City;
            IsReadOnly = parameter.IsReadonly;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getProductTask = productsManager.GetProductAsync((int)id, city);
            var getRelatedProductTask = productsManager.GetRelatedProductsAsync((int)id, city);

            await Task.WhenAll(getProductTask, getRelatedProductTask);

            product = getProductTask.Result.Data;
            var relatedProducts = getRelatedProductTask.Result.Data.ToList();
            toppings = product?.Params?.AvailableToppings?.ToList() ?? new List<Topping>();

            var viewModels = relatedProducts.Select(product => new ProductItemViewModel(cartManager, product, city, RefreshDataAsync)).ToList();
            RelatedItems.AddRange(viewModels);
            await RaiseAllPropertiesChanged();
        }

        protected override Task RefreshDataAsync()
        {
            hasChanged = true;
            return base.RefreshDataAsync();
        }

        private async Task AddToCartAsync()
        {
            try
            {
                if (toppings.Count == 0)
                {
                    return;
                }
                
                var navigationParams = new ToppingNavigationParameters(toppings, AppStrings.MakeItTastier, product!.Currency.Symbol);
                var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
                if (result is null)
                {
                    return;
                }

                toppings = result;
            }
            finally
            {
                StepperViewModel.AddCommand.Execute();
                IsHiddenStepper = false;
            }
        }

        private async Task OnCountChangedAsync(int previousCount, int newCount)
        {
            IsHiddenStepper = newCount == 0;
            var step = newCount - previousCount;

            var selectedToppings = toppings.Where(topping => topping.CountInBasket > 0).ToArray();
            var response = await cartManager.UpdateProductInCartAsync(city, product!.Id, product?.Uid, step, selectedToppings);
            if (response.Data is null)
            {
                return;
            }

            hasChanged = true;
            Messenger.Publish(new RefreshCartMessage(this));
            product!.Uid = response.Data.Uid;
        }
    }
}