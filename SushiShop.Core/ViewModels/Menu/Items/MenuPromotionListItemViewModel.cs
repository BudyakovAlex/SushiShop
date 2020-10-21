using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class MenuPromotionListItemViewModel : BaseViewModel
    {
        public MenuPromotionListItemViewModel(MenuPromotionItemViewModel[] items)
        {
            Items = items;
        }

        public MenuPromotionItemViewModel[] Items { get; }

        public int ItemsCount => Items.Length;
    }
}
