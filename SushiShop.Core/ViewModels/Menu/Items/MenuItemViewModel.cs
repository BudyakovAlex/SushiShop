using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuItemViewModel : BaseViewModel
    {
        public MenuItemViewModel(MenuItem menuItem)
        {
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);

            Title = menuItem.Title;
            ImageUrl = menuItem.ImageUri;
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