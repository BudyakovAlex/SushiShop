using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Shops.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class MetroSectionViewModel : BaseViewModel
    {
        private readonly IShopsManager shopsManager;
        private readonly IUserSession userSession;

        private Dictionary<string, MetroShop[]>? metroShopsMappings;

        public MetroSectionViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;

            Items = new MvxObservableCollection<MetroItemViewModel>();
        }

        public MvxObservableCollection<MetroItemViewModel> Items { get; }

        public async Task InitializeAsync()
        {
            var city = userSession.GetCity();
            var response = await shopsManager.GetMetroShopsAsync(city?.Name);
            if (!response.IsSuccessful)
            {
                return;
            }

            metroShopsMappings = response.Data;
            var viewModels = metroShopsMappings.Keys.Select(key => new MetroItemViewModel(key, GoToShopAsync)).ToArray();
            Items.AddRange(viewModels);
        }

        private Task GoToShopAsync(MetroItemViewModel itemViewModel)
        {
            var shops = metroShopsMappings?.GetValueOrDefault(itemViewModel.Text);
            if (shops is null || shops.Length == 0)
            {
                return Task.CompletedTask;
            }

            return NavigationManager.NavigateAsync<ShopsNearMetroViewModel>();
        }
    }
}