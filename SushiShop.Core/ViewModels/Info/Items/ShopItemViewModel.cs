using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;

namespace SushiShop.Core.ViewModels.Info.Items
{
    public class ShopItemViewModel : SelectableItemViewModel
    {
        public ShopItemViewModel(string text, bool isSelected = false) : base(text, isSelected)
        {
        }
    }
}