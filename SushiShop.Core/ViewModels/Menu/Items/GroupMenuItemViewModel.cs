using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models;
using SushiShop.Core.Extensions;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupMenuItemViewModel : BaseViewModel
    {
        public GroupMenuItemViewModel(Sticker sticker)
        {
            Title = sticker.Type.ToLocalizedString();
            Type = sticker.Type;

            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public string Title { get; }

        public StickerType Type { get; }

        public IMvxCommand ShowDetailsCommand { get; }

        private Task ShowDetailsAsync()
        {
            return Task.CompletedTask;
        }
    }
}