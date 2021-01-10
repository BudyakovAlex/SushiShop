using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Shops;
using SushiShop.Core.Messages;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
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

        private ShopsOnMapSectionViewModel? shopsOnMapSectionViewModel;
        private ShopsListSectionViewModel? shopsListSectionViewModel;
        private MetroSectionViewModel? metroSectionViewModel;
        private ShopItemViewModel? savedSelectedItem;

        private int previousSelectedIndex;

        public ShopsViewModel(IShopsManager shopsManager, IUserSession userSession)
        {
            this.shopsManager = shopsManager;
            this.userSession = userSession;

            Messenger.Subscribe<CityChangedMessage>(OnCityChnaged).DisposeWith(Disposables);
        }

        public List<string> TabsTitles { get; } = new List<string>();

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set => SetProperty(ref selectedIndex, value, OnSelectedIndexChanged);
        }

        public override Task InitializeAsync()
        {
            return Task.WhenAll(base.InitializeAsync(), RefreshDataAsync());
        }

        protected override async Task RefreshDataAsync()
        {
            var city = userSession.GetCity();
            var isMetroAvailable = city?.IsMetroAvailable ?? true;

            Items.ReplaceWith(ProduceSectionsViewModels(city, isMetroAvailable));
            TabsTitles.ReplaceWith(ProduceSectionsTitles(isMetroAvailable));

            var getShopsTask = shopsManager.GetShopsAsync(city?.Name);
            var getMetroShopsTask = shopsManager.GetMetroShopsAsync(city?.Name);
            await Task.WhenAll(getShopsTask, getMetroShopsTask, RaisePropertyChanged(nameof(TabsTitles)));

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

        public override void ViewAppearing()
        {
            base.ViewAppeared();

            if (selectedIndex == 0)
            {
                ShowSelectedItem();
            }
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();

            if (previousSelectedIndex != 0)
            {
                return;
            }

            HideSelectedItem();
        }

        private void ShowSelectedItem()
        {
            if (shopsOnMapSectionViewModel is null)
            {
                return;
            }

            shopsOnMapSectionViewModel.SelectedItem = savedSelectedItem;
            savedSelectedItem = null;
        }

        private void HideSelectedItem()
        {
            if (shopsOnMapSectionViewModel is null)
            {
                return;
            }

            savedSelectedItem = shopsOnMapSectionViewModel.SelectedItem;
            shopsOnMapSectionViewModel.SelectedItem = null;
        }

        private IEnumerable<BaseViewModel> ProduceSectionsViewModels(City? city, bool isMetroAvailable)
        {
            var coordinates = city is null
                ? new Coordinates(Constants.Map.MapStartPointLongitude, Constants.Map.MapStartPointLatitude)
                : new Coordinates(city.Longitude, city.Latitude);
            yield return shopsOnMapSectionViewModel = new ShopsOnMapSectionViewModel(coordinates, city?.ZoomFactor ?? Constants.Map.DefaultZoomFactor);
            yield return shopsListSectionViewModel = new ShopsListSectionViewModel().DisposeWith(Disposables);

            if (isMetroAvailable)
            {
                yield return metroSectionViewModel = new MetroSectionViewModel();
            }
        }

        private IEnumerable<string> ProduceSectionsTitles(bool isMetroAvailable)
        {
            yield return AppStrings.OnMap;
            yield return AppStrings.List;

            if (isMetroAvailable)
            {
                yield return AppStrings.Metro;
            }
        }

        private void OnSelectedIndexChanged()
        {
            if (selectedIndex == 0)
            {
                ShowSelectedItem();
            }

            if (previousSelectedIndex == 0)
            {
                HideSelectedItem();
            }

            previousSelectedIndex = selectedIndex;
        }

        private void OnCityChnaged(CityChangedMessage message)
        {
            RefreshDataCommand.Execute();
        }
    }
}