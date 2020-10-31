using System.Linq;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Managers.Products;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Menu.Items;

namespace SushiShop.Core.ViewModels.Menu
{
    public class ProductViewModel : BaseItemsPageViewModel<ProductItemViewModel, Category>
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

        public override void Prepare(Category parameter)
        {
            category = parameter;
            Filters = category.Children is null
                ? new string[] { "Все" }
                : category.Children.SubCategories.Select(category => category.PageTitle).Prepend("Все").ToArray();
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
            if (SelectedFilterIndex == 0)
            {
                Items.ReplaceWith(allItems);
            }
            else
            {
            }
        }
    }
}
