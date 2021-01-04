using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Info.Items;

namespace SushiShop.Core.ViewModels.Info.Sections
{
    public class MetroSectionViewModel : BaseViewModel
    {
        public MetroSectionViewModel()
        {
            Items = new MvxObservableCollection<MetroItemViewModel>();
        }

        public MvxObservableCollection<MetroItemViewModel> Items { get; }
    }
}