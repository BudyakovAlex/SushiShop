using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.NavigationParameters;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class CategoryMenuItemViewModel : BaseViewModel
    {
        private readonly Category category;

        public CategoryMenuItemViewModel(Category category)
        {
            this.category = category;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxAsyncCommand ShowDetailsCommand { get; }

        public string Title => category.PageTitle;
        public string ImageUrl => category.CategoryIcon?.JpgUrl ?? string.Empty;

        private Task ShowDetailsAsync()
        {
            var parameters = new ProductNavigationParameters(category);
            return NavigationManager.NavigateAsync<ProductViewModel, ProductNavigationParameters>(parameters);
        }
    }
}