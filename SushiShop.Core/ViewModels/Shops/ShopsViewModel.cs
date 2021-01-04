using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Info
{
    public class ShopsViewModel : BaseItemsPageViewModel<BaseViewModel>
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;

        private ShopsOnMapSectionViewModel? shopsOnMapViewModel;
        private ShopsListSectionViewModel? shopsListViewModel;
        private MetroSectionViewModel? metroSectionViewModel;

        public ShopsViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var city = userSession.GetCity();
            var isMetroAvailable = city?.IsMetroAvailable ?? true;
            Items.AddRange(ProduceSectionsViewModels(isMetroAvailable));

            var getShopsTask = shopsManager.GetShopsAsync(city?.Name);
            var initializeMetroTask = metroSectionViewModel?.InitializeAsync() ?? Task.CompletedTask;

            await Task.WhenAll(getShopsTask, initializeMetroTask);

            if (!getShopsTask.Result.IsSuccessful)
            {
                return;
            }

            var viewModels = getShopsTask.Result.Data.Select(item => new ShopItemViewModel(item)).ToArray();
            shopsOnMapViewModel!.Items.AddRange(viewModels);
            shopsListViewModel!.Items.AddRange(viewModels);
        }

        private IEnumerable<BaseViewModel> ProduceSectionsViewModels(bool isMetroAvailable)
        {
            yield return shopsOnMapViewModel = new ShopsOnMapSectionViewModel();
            yield return shopsListViewModel = new ShopsListSectionViewModel();

            if (isMetroAvailable)
            {
                yield return metroSectionViewModel = new MetroSectionViewModel(shopsManager, userSession);
            }
        }
    }
}