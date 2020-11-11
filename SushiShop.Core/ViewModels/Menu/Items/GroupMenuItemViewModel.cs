using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.NavigationParameters;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupMenuItemViewModel : BaseViewModel
    {
        private readonly Sticker sticker;

        public GroupMenuItemViewModel(Sticker sticker)
        {
            this.sticker = sticker;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxCommand ShowDetailsCommand { get; }

        public StickerType Type => sticker.Type;
        public string Title => sticker.Title;
        public string ImageUrl => sticker.ImageUrl;

        private Task ShowDetailsAsync()
        {
            var parameters = new ProductsNavigationParameters(sticker);
            return NavigationManager.NavigateAsync<ProductsViewModel, ProductsNavigationParameters>(parameters);
        }
    }
}