using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupMenuItemViewModel : BaseViewModel
    {
        public GroupMenuItemViewModel(Sticker sticker)
        {
        }

        public ActionType ActionType { get; }
    }
}