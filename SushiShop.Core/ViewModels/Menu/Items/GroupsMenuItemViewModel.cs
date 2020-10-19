using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupsMenuItemViewModel : BaseViewModel
    {
        public GroupsMenuItemViewModel(IEnumerable<GroupMenuItemViewModel> groupMenuItemViewModels)
        {
            Items = new MvxObservableCollection<GroupMenuItemViewModel>(groupMenuItemViewModels);
        }

        public MvxObservableCollection<GroupMenuItemViewModel> Items { get; }
    }
}