using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Menu;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class CategoryMenuItemViewModel : BaseViewModel
    {
        public CategoryMenuItemViewModel(Category category)
        {
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public string Title { get; }
        public string ImageUrl { get; }
        public IMvxCommand ShowDetailsCommand { get; }

        private Task ShowDetailsAsync()
        {
            return Task.CompletedTask;
        }
    }
}