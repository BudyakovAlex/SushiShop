using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.ViewModels.Info.Sections;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Info
{
    public class ShopsViewModel : BaseItemsPageViewModel<BaseViewModel>
    {
        public ShopsViewModel()
        {
            Items.AddRange(new BaseViewModel []
            {
                new ShopsOnMapSectionViewModel(),
                new ShopsListViewModel(),
                new MetroSectionViewModel()
            });
        }

        //TODO: add loading data here
        public override Task InitializeAsync()
        {
            return base.InitializeAsync();
        }
    }
}