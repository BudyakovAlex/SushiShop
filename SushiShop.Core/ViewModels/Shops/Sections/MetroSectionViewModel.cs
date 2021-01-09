using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Shops.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class MetroSectionViewModel : BaseViewModel
    {
        private Dictionary<string, MetroShop[]>? metroShopsMappings;

        public MetroSectionViewModel()
        {
            Items = new MvxObservableCollection<MetroItemViewModel>();
        }

        public MvxObservableCollection<MetroItemViewModel> Items { get; }

        public void SetMetroShops(Dictionary<string, MetroShop[]>? metroShopsMappings)
        {
            this.metroShopsMappings = metroShopsMappings;
            var viewModels = metroShopsMappings?.Keys.Select(key => new MetroItemViewModel(key, GoToShopAsync)).ToArray() ?? Array.Empty<MetroItemViewModel>();
            Items.ReplaceWith(viewModels);
        }

        private Task GoToShopAsync(MetroItemViewModel itemViewModel)
        {
            var shops = metroShopsMappings?.GetValueOrDefault(itemViewModel.Text);
            if (shops is null || shops.Length == 0)
            {
                return Task.CompletedTask;
            }

            var navigationParameter = new ShopsNearMetroNavigationParameters(shops, itemViewModel.Text);
            return NavigationManager.NavigateAsync<ShopsNearMetroViewModel, ShopsNearMetroNavigationParameters>(navigationParameter);
        }
    }
}