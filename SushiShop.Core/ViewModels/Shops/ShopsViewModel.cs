using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Shops.Sections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Info
{
    public class ShopsViewModel : BaseItemsPageViewModel<BaseViewModel>
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;

        private ShopsOnMapSectionViewModel? shopsOnMapSectionViewModel;
        private ShopsListSectionViewModel? shopsListSectionViewModel;
        private MetroSectionViewModel? metroSectionViewModel;

        public ShopsViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;
        }

        public override Task InitializeAsync()
        {
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        protected override async Task RefreshDataAsync()
        {
            var city = userSession.GetCity();
            var isMetroAvailable = city?.IsMetroAvailable ?? true;
            Items.AddRange(ProduceSectionsViewModels(isMetroAvailable));

            var getShopsTask = shopsManager.GetShopsAsync(city?.Name);
            var getMetroShopsTask = shopsManager.GetMetroShopsAsync(city?.Name);
            await Task.WhenAll(getShopsTask, getMetroShopsTask);

            if (!getShopsTask.Result.IsSuccessful
                || !getMetroShopsTask.Result.IsSuccessful)
            {
                return;
            }

            shopsListSectionViewModel?.SetShops(getShopsTask.Result.Data);
            shopsOnMapSectionViewModel?.SetShops(getShopsTask.Result.Data);

            shopsListSectionViewModel?.SetMetroShops(getMetroShopsTask.Result.Data);
            metroSectionViewModel?.SetMetroShops(getMetroShopsTask.Result.Data);
        }

        private IEnumerable<BaseViewModel> ProduceSectionsViewModels(bool isMetroAvailable)
        {
            yield return shopsOnMapSectionViewModel = new ShopsOnMapSectionViewModel();
            yield return shopsListSectionViewModel = new ShopsListSectionViewModel().DisposeWith(Disposables);

            if (isMetroAvailable)
            {
                yield return metroSectionViewModel = new MetroSectionViewModel();
            }
        }
    }
}