using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Extensions;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupMenuItemViewModel : BaseViewModel
    {
        public GroupMenuItemViewModel(Sticker sticker)
        {
            Type = sticker.Type;
            Title = sticker.Title;
            ImageUrl = sticker.ImageUrl;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public IMvxCommand ShowDetailsCommand { get; }

        public StickerType Type { get; }

        public string Title { get; }

        public string ImageUrl { get; }

        private Task ShowDetailsAsync()
        {
            return Task.CompletedTask;
        }
    }
}