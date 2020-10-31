using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel : BasePageViewModel<CardProductNavigationParameters>
    {
        private readonly IProductsManager productsManager;

        private Product? product;
        private List<Topping> toppings;

        private int id;
        private string? city;

        public ProductDetailsViewModel(IProductsManager productsManager)
        {
            this.productsManager = productsManager;

            toppings = new List<Topping>();
            RelatedItems = new MvxObservableCollection<Product>();
            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
        }

        public IMvxCommand AddToCartCommand { get; }

        public string BackgroungImageUrl => product?.MainImageInfo?.OriginalUrl ?? string.Empty;
        public string Protein => product?.Params?.Proteins ?? string.Empty;
        public string Fats => product?.Params?.Fats ?? string.Empty;
        public string Carbohydrates => product?.Params?.Carbons ?? string.Empty;
        public string Calories => product?.Params?.CalorificValue ?? string.Empty;
        public string Title => product?.PageTitle ?? string.Empty;
        public string Description => product?.IntroText ?? string.Empty;
        public string Weight => product?.Params?.Weight ?? string.Empty;
        public string Price => product?.Price.ToString() ?? string.Empty;
        public string OldPrice => product?.Price.ToString() ?? string.Empty;

        public MvxObservableCollection<Product> RelatedItems { get; }

        public override void Prepare(CardProductNavigationParameters parameter)
        {
            id = parameter.Id;
            city = parameter.City;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getProductTask = productsManager.GetProductAsync(id, city);
            var getRelatedProductTask = productsManager.GetRelatedProductsAsync(id, city);

            await Task.WhenAll(getProductTask, getRelatedProductTask);

            product = getProductTask.Result.Data;
            var relatedProducts = getRelatedProductTask.Result.Data.ToList();
            toppings = product?.Params?.AvailableToppings.ToList() ?? new List<Topping>();

            RelatedItems.AddRange(relatedProducts);
            await RaiseAllPropertiesChanged();
        }

        private async Task AddToCartAsync()
        {
            var navigationParams = new ToppingNavigationParameters(toppings);
            var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
            if (result is null)
            {
                return;
            }

            toppings = result;
        }
    }
}