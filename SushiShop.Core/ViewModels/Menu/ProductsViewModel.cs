using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Managers.Cart;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Menu.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu
{
    public class ProductsViewModel : BaseItemsPageViewModel<FilteredProductsViewModel, ProductsNavigationParameters>
    {
        private readonly IProductsManager productsManager;
        private readonly ICartManager cartManager;
        private readonly IUserSession userSession;

        private Category? category;
        private Sticker? sticker;

        public ProductsViewModel(
            IProductsManager productsManager,
            ICartManager cartManager,
            IUserSession userSession)
        {
            this.productsManager = productsManager;
            this.cartManager = cartManager;
            this.userSession = userSession;

            Messenger.Subscribe<CartProductChangedMessage>(OnCartProductChangedMessage).DisposeWith(Disposables);
            Messenger.Subscribe<RefreshProductsMessage>(OnRefreshProductsMessage).DisposeWith(Disposables);
        }

        public string? Title => category?.PageTitle ?? sticker?.Title;
        public List<string> Filters { get; private set; } = new List<string>();

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

        public override Task InitializeAsync()
        {
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        protected override async Task RefreshDataAsync()
        {
            var city = userSession.GetCity();
            var response = await productsManager.GetProductsByCategoryAsync(category?.Id, city?.Name, sticker?.Type);
            if (!response.IsSuccessful)
            {
                return;
            }

            var allItems = response.Data.Select(product => new ProductItemViewModel(cartManager, product, city?.Name, RefreshDataAsync) { ExecutionStateWrapper = ExecutionStateWrapper }).ToArray();
            var filteredItems = Filters.IsEmpty()
                ? new FilteredProductsViewModel[] { new FilteredProductsViewModel(allItems, RefreshDataAsync) }
                : Filters.Select((_, index) => ProduceItemsByFilter(allItems, index)).ToArray();

            Items.SwitchTo(filteredItems);
        }

        private FilteredProductsViewModel ProduceItemsByFilter(ProductItemViewModel[] items, int index)
        {
            if (index == 0)
            {
                return new FilteredProductsViewModel(items, RefreshDataAsync);
            }

            var parentId = category!.Children!.SubCategories[index - 1].Id;
            var filteredItems = items.Where(item => item.ParentId == parentId).ToArray();

            return new FilteredProductsViewModel(filteredItems, RefreshDataAsync);
        }

        private void OnRefreshProductsMessage(RefreshProductsMessage message)
        {
            if (message.Sender is ProductItemViewModel)
            {
                return;
            }

            _ = RefreshDataAsync();
        }

        private void OnCartProductChangedMessage(CartProductChangedMessage message)
        {
            _ = RefreshDataAsync();
        }
    }
}
