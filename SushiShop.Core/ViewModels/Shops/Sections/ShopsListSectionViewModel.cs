using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Extensions;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Shops.Sections
{
    public class ShopsListSectionViewModel : BaseViewModel, IDisposable
    {
        private readonly Func<MetroShop[], string, Task> goToShopFunc;
        private readonly Func<Shop, Task> goToMapFunc;
        private readonly Func<Shop, Task> confirmSelectionFunc;
        private readonly bool isSelectionMode;

        private Dictionary<string, MetroShop[]>? metroShopsMappings;
        private ShopItemViewModel[] orignalSource;

        private bool isDisposed;

        public ShopsListSectionViewModel(
            Func<MetroShop[], string, Task> goToShopFunc,
            Func<Shop, Task> goToMapFunc,
            Func<Shop, Task> confirmSelectionFunc,
            bool isSelectionMode)
        {
            this.goToShopFunc = goToShopFunc;
            this.goToMapFunc = goToMapFunc;
            this.confirmSelectionFunc = confirmSelectionFunc;
            this.isSelectionMode = isSelectionMode;

            Disposables = new CompositeDisposable();
            Items = new MvxObservableCollection<ShopItemViewModel>();
            NearestMetro = new MvxObservableCollection<MetroItemViewModel>();
            orignalSource = Array.Empty<ShopItemViewModel>();

            NearestMetro.SubscribeToCollectionChanged(OnNearestMetroCollectionChanged).DisposeWith(Disposables);

            RemoveFocusInteraction = new MvxInteraction();
            ClearNearestMetroCommand = new MvxCommand(() => NearestMetro.Clear());
            ShowResultsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowResultsAsync);
        }

        public ICommand ClearNearestMetroCommand { get; }

        public IMvxCommand ShowResultsCommand { get; }

        public MvxInteraction RemoveFocusInteraction { get; }

        private string query = string.Empty;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                ShowResultsCommand.Execute();
            }
        }

        public MvxObservableCollection<ShopItemViewModel> Items { get; }

        public bool IsNearestMetroNotEmpty => NearestMetro.Count > 0;

        public MvxObservableCollection<MetroItemViewModel> NearestMetro { get; }

        protected CompositeDisposable Disposables { get; }

        public void SetShops(Shop[] shops)
        {
            var viewModels = shops.Select(item => new ShopItemViewModel(
                item,
                GoToMapAsync,
                confirmSelectionFunc,
                isSelectionMode,
                showNearestMetroAction: ShowNearestMetro)).ToArray();

            orignalSource = viewModels;
            Items.ReplaceWith(orignalSource);
        }

        public void SetMetroShops(Dictionary<string, MetroShop[]>? metroShopsMappings)
        {
            this.metroShopsMappings = metroShopsMappings;
        }

        private Task GoToMapAsync(Shop shop)
        {
            RemoveFocusInteraction.Raise();
            return goToMapFunc(shop);
        }

        private void OnNearestMetroCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsNearestMetroNotEmpty));
        }

        private void ShowNearestMetro(Shop shop)
        {
            RemoveFocusInteraction.Raise();
            if (shop.Metro.Length == 0)
            {
                UserDialogs.Instance.Alert(AppStrings.NoNearestMetro);
                return;
            }

            var metroViewModels = shop.Metro.Select(metro => new MetroItemViewModel(metro.Name!, GoToShopAsync)).ToArray();
            NearestMetro.ReplaceWith(metroViewModels);
        }

        private Task GoToShopAsync(MetroItemViewModel itemViewModel)
        {
            NearestMetro.Clear();

            var shops = metroShopsMappings?.GetValueOrDefault(itemViewModel.Text);
            if (shops is null || shops.Length == 0)
            {
                return Task.CompletedTask;
            }

            return goToShopFunc?.Invoke(shops, itemViewModel.Text) ?? Task.CompletedTask;
        }

        private Task ShowResultsAsync()
        {
            if (string.IsNullOrEmpty(Query) ||
                Query.Length < 2)
            {
                Items.ReplaceWith(orignalSource);
                return Task.CompletedTask;
            }

            var itemsWithQueryOnStart = orignalSource.Where(item => item.Text.ToLower().StartsWith(Query.ToLower())).ToList();
            var itemsWithQueryInMiddle = orignalSource.Where(item => item.Text.ToLower().Contains(Query.ToLower())).ToList();

            var mergedItems = itemsWithQueryOnStart.Union(itemsWithQueryInMiddle)
                                                   .DistinctBy(item => item.Text)
                                                   .ToList();
            Items.ReplaceWith(mergedItems);
            return Task.CompletedTask;
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