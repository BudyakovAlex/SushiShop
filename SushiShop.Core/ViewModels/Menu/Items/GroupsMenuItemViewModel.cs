using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using System.Collections.Generic;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupsMenuItemViewModel : BaseViewModel
    {
        public GroupsMenuItemViewModel(IEnumerable<GroupMenuItemViewModel> groupMenuItemViewModels)
        {
            Items = groupMenuItemViewModels;
        }

        public IEnumerable<GroupMenuItemViewModel> Items { get; }
    }
}