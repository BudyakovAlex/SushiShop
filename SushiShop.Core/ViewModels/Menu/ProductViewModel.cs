using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Menu
{
    public class ProductViewModel : BaseItemsPageViewModel<ProductItemViewModel, ProductNavigationParameters>
    {
        private readonly IProductsManager productsManager;
        private readonly IUserSession userSession;

        private Category? category;
        private ProductItemViewModel[] allItems = new ProductItemViewModel[0];

        public ProductViewModel(IProductsManager productsManager, IUserSession userSession)
        {
            this.productsManager = productsManager;
            this.userSession = userSession;
        }

        public string Title => category!.PageTitle;
        public string[] Filters { get; private set; } = new string[0];

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
            set => SetProperty(ref selectedFilterIndex, value, () =>
            {
                SelectedFilterIndexChanged();
            });
        }

        public bool IsFiltersVisible => Filters.IsNotEmpty();

        public override void Prepare(ProductNavigationParameters parameter)
        {
            category = parameter.Category;

            var subCategories = parameter.Category?.Children?.SubCategories;
            if (subCategories != null && subCategories.Length > 0)
            {
                Filters = subCategories
                    .Select(category => category.PageTitle)
                    .Prepend("Все")
                    .ToArray();
            }
        }

        public override async Task InitializeAsync()
        {
            IsLoading = true;

            await base.InitializeAsync();

            var city = userSession.GetCity();
            var response = await productsManager.GetProductsByCategoryAsync(category!.Id, city?.Name, stickerType: null);
            if (response.IsSuccessful)
            {
                allItems = response.Data.Select(product => new ProductItemViewModel(product)).ToArray();
                Items.ReplaceWith(allItems);
            }

            IsLoading = false;
        }

        private void SelectedFilterIndexChanged()
        {
            var index = SelectedFilterIndex;
            if (index == 0)
            {
                Items.ReplaceWith(allItems);
            }
            else
            {
                var parentId = category!.Children!.SubCategories[index - 1].Id;
                var items = allItems.Where(item => item.ParentId == parentId).ToArray();
                Items.ReplaceWith(items);
            }
        }
    }
}
