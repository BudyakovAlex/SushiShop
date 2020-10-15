using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using System.Collections.Generic;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class BannerMenuItemViewModel : BaseViewModel
    {
        public BannerMenuItemViewModel(IEnumerable<MenuItemViewModel> menuItemViewModels)
        {
            Items = menuItemViewModels;
        }

        public IEnumerable<MenuItemViewModel> Items { get; }
    }
}