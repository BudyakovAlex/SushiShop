using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Core.ViewModels.Cities.Items;

namespace SushiShop.Core.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel : BasePageViewModel<CardProductNavigationParameters>
    {
        private readonly IProductsManager productsManager;
        private Product? product;

        private int id;
        private string? city;

        public ProductDetailsViewModel(IProductsManager productsManager)
        {
            this.productsManager = productsManager;
            ToppingViewModels = new List<ToppingItemViewModel>();

            AddToCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, AddToCartAsync);
        }

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
            List<Product> relatedProducts = getRelatedProductTask.Result.Data.ToList();

            //SimpleItems.Add(new ToppingItemViewModel());

            _ = RaisePropertyChanged(nameof(product));
        }

        public MvxObservableCollection<Product> SimpleItems { get; }

        public IMvxCommand AddToCartCommand { get; }
        
        public string BackgroungImageUrl => product?.MainImageInfo?.OriginalUrl ?? string.Empty;
        public string Protein => product?.Params?.Proteins ?? string.Empty;
        public string Fats => product?.Params?.Fats ?? string.Empty;
        public string Carbohydrates => product?.Params?.Carbons ?? string.Empty;
        public string Ccal => product?.Params?.CalorificValue ?? string.Empty;
        public string Title => product?.PageTitle ?? string.Empty;
        public string Description => product?.IntroText ?? string.Empty;
        public string Weight => product?.Params?.Weight ?? string.Empty;
        public string Price => product?.Price.ToString() ?? string.Empty;
        public string OldPrice => product?.Price.ToString() ?? string.Empty;
        public List<ToppingItemViewModel> ToppingViewModels { get; set; }
        private async Task AddToCartAsync()
        {
            var navigationParams = new ToppingNavigationParameters(product.Params?.AvailableToppings.ToList() ?? new List<Topping> {});
            var result = await NavigationManager.NavigateAsync<ToppingsViewModel, ToppingNavigationParameters, List<Topping>>(navigationParams);
            if (result is null)
            {
                return;
            }
        }
    }
}