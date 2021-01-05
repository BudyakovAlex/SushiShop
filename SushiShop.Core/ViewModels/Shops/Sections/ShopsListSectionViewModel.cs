using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.ViewModels.Shops.Items;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class ShopsListSectionViewModel : BaseViewModel, IDisposable
    {
        private Dictionary<string, MetroShop[]>? metroShopsMappings;

        private bool isDisposed;

        public ShopsListSectionViewModel()
        {
            Disposables = new CompositeDisposable();
            Items = new MvxObservableCollection<ShopItemViewModel>();
            NearestMetro = new MvxObservableCollection<MetroItemViewModel>();

            NearestMetro.SubscribeToCollectionChanged(OnNearestMetroCollectionChanged).DisposeWith(Disposables);
        }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }

        public bool IsNearestMetroNotEmpty => NearestMetro.Count > 0;

        public MvxObservableCollection<MetroItemViewModel> NearestMetro { get; }

        protected CompositeDisposable Disposables { get; }

        public void SetShops(Shop[] shops)
        {
            var viewModels = shops.Select(item => new ShopItemViewModel(item, showNearestMetroAction: ShowNearestMetro)).ToArray();
            Items.ReplaceWith(viewModels);
        }

        public void SetMetroShops(Dictionary<string, MetroShop[]>? metroShopsMappings)
        {
            this.metroShopsMappings = metroShopsMappings;
        }

        private void OnNearestMetroCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsNearestMetroNotEmpty));
        }

        private Task GoToShopAsync(MetroItemViewModel itemViewModel)
        {
            var shops = metroShopsMappings?.GetValueOrDefault(itemViewModel.Text);
            if (shops is null || shops.Length == 0)
            {
                return Task.CompletedTask;
            }

            var navigationParameter = new ShopsNearMetroNavigationParameters(shops);
            return NavigationManager.NavigateAsync<ShopsNearMetroViewModel, ShopsNearMetroNavigationParameters>(navigationParameter);
        }

        private void ShowNearestMetro(Shop shop)
        {
            var metroViewModels = shop.Metro.Select(metro => new MetroItemViewModel(metro.Name!, GoToShopAsync)).ToArray();
            NearestMetro.ReplaceWith(metroViewModels);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || isDisposed)
            {
                return;
            }

            Disposables.Dispose();
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}