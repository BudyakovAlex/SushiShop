using System.Collections.Generic;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class GroupsMenuItemViewModel : BaseViewModel
    {
        public GroupsMenuItemViewModel(IList<GroupMenuItemViewModel> groupMenuItemViewModels)
        {
            Items = new MvxObservableCollection<GroupMenuItemViewModel>(groupMenuItemViewModels);
        }

        public MvxObservableCollection<GroupMenuItemViewModel> Items { get; }

        public int ItemsCount => Items.Count;
    }
}