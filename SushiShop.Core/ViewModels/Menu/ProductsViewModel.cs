using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Menu
{
    public class ProductsViewModel : BaseItemsPageViewModel<FilteredProductsViewModel, ProductsNavigationParameters>
    {
        private readonly IProductsManager productsManager;
        private readonly IUserSession userSession;

        private Category? category;
        private Sticker? sticker;

        public ProductsViewModel(IProductsManager productsManager, IUserSession userSession)
        {
            this.productsManager = productsManager;
            this.userSession = userSession;
        }

        public string? Title => category?.PageTitle ?? sticker?.Title;
        public List<string> Filters { get; private set; } = new List<string>();

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            private set => SetProperty(ref isLoading, value);
        }

        private int selectedFilterIndex;
        public int SelectedFilterIndex
        {
            get => selectedFilterIndex;
            set => SetProperty(ref selectedFilterIndex, value);
        }

        public bool IsFiltersVisible => Filters.IsNotEmpty();

        public override void Prepare(ProductsNavigationParameters parameter)
        {
            category = parameter.Category;
            sticker = parameter.Sticker;

            var subCategories = parameter.Category?.Children?.SubCategories;
            if (subCategories != null && subCategories.Length > 0)
            {
                Filters = subCategories
                    .Select(category => category.PageTitle)
                    .Prepend(AppStrings.All)
                    .ToList();
            }
        }

        public override async Task InitializeAsync()
        {
            IsLoading = true;

            await base.InitializeAsync();

            var city = userSession.GetCity();
            var response = await productsManager.GetProductsByCategoryAsync(category?.Id, city?.Name, sticker?.Type);
            if (response.IsSuccessful)
            {
                var allItems = response.Data.Select(product => new ProductItemViewModel(product)).ToArray();
                var filteredItems = Filters.IsEmpty()
                    ? new FilteredProductsViewModel[] { new FilteredProductsViewModel(allItems) }
                    : Filters.Select((_, index) => ProduceItemsByFilter(allItems, index)).ToArray();

                Items.ReplaceWith(filteredItems);
            }

            IsLoading = false;
        }

        private FilteredProductsViewModel ProduceItemsByFilter(ProductItemViewModel[] items, int index)
        {
            if (index == 0)
            {
                return new FilteredProductsViewModel(items);
            }

            var parentId = category!.Children!.SubCategories[index - 1].Id;
            var filteredItems = items.Where(item => item.ParentId == parentId).ToArray();

            return new FilteredProductsViewModel(filteredItems);
        }
    }
}
