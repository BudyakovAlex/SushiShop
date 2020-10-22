using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Common;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Abstract;
using SushiShop.Core.ViewModels.Cities.Items;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Cities
{
    public class SelectCityViewModel : SelectablePageItemsViewModel<CityItemViewModel, SelectCityNavigationParameters, int>
    {
        private readonly ICitiesManager citiesManager;

        public SelectCityViewModel(ICitiesManager citiesManager)
        {
            this.citiesManager = citiesManager;
        }

        public override string QueryPlaceholder => AppStrings.SearchSity;

        protected override async Task<List<CityItemViewModel>> LoadDataAsync(List<int> selectedItemIds)
        {
            var response = await citiesManager.GetCitiesAsync();
            var items = response.Data.Select(city => new CityItemViewModel(city)).ToList();
            if (selectedItemIds.Count == 0)
            {
                var foundItem = items.FirstOrDefault(item => item.City.Name.ToLowerInvariant().Equals(Constants.Menu.DefaultCityName.ToLowerInvariant()));
                if (foundItem != null)
                {
                    foundItem.IsSelected = true;
                }
            }
            else
            {
                items.ForEach(item => item.IsSelected = selectedItemIds.Contains(item.City.Id));
            }

            return items;
        }
    }
}