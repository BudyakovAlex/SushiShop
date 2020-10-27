using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Products;
using SushiShop.Core.Data.Models.Toppings;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.CardProduct.Items;

namespace SushiShop.Core.ViewModels.CardProduct
{
    public class CardProductViewModel : BasePageViewModel<CardProductNavigationParameters>
    {
        private readonly IProductsManager productsManager;
        private Product? product;

        private int id;
        private string? city;

        public CardProductViewModel(IProductsManager productsManager)
        {
            this.productsManager = productsManager;
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
            ToppingViewModels = new List<ToppingViewModel>();

            var getProductTask = productsManager.GetProductAsync(id, city);
            var getRelatedProductTask = productsManager.GetRelatedProductsAsync(id, city);

            await Task.WhenAll(getProductTask, getRelatedProductTask);

            product = getProductTask.Result.Data;
            List<Product> RelatedProducts = getRelatedProductTask.Result.Data.ToList();

            for (var i = 0; i < RelatedProducts.Count; i++)
            {
                ToppingViewModels.Add(new ToppingViewModel(
                    new Topping(
                        RelatedProducts[i].Params.AvailableToppings[i].Id,
                        RelatedProducts[i].Params.AvailableToppings[i].PageTitle,
                        RelatedProducts[i].Params.AvailableToppings[i].Price,
                        RelatedProducts[i].Params.AvailableToppings[i].CountInBasket)));
            }

            _ = RaisePropertyChanged(nameof(product));
        }

        public IMvxCommand AddToCartCommand { get; }

        public string ProteinTitle => GetProteinTitle();
        public string FatsTitle => GetFatsTitle();
        public string CarbohydratesTitle => GetCarbohydratesTitle();
        public string CcalTitle => GetCcalTitle();

        public string BackgroungImageUrl => product?.MainImageInfo.OriginalUrl ?? string.Empty;
        public string Protein => product?.Params?.Proteins ?? string.Empty;
        public string Fats => product?.Params?.Fats ?? string.Empty;
        public string Carbohydrates => product?.Params?.Carbons ?? string.Empty;
        public string Ccal => product?.Params?.CalorificValue ?? string.Empty;
        public string Title => product?.PageTitle ?? string.Empty;
        public string Description => product?.IntroText ?? string.Empty;
        public string WeightProduct => product?.Params?.Weight ?? string.Empty;
        public string Price => product?.Price.ToString() ?? string.Empty;
        public string OldPrice => product?.Price.ToString() ?? string.Empty;
        public List<ToppingViewModel> ToppingViewModels { get; set; }
        private async Task AddToCartAsync()
        {
        }

        private string GetProteinTitle() => AppStrings.Protein;
        private string GetFatsTitle() => AppStrings.Fats;
        private string GetCarbohydratesTitle() => AppStrings.Carbohydrates;
        private string GetCcalTitle() => AppStrings.Ccal;
    }
}