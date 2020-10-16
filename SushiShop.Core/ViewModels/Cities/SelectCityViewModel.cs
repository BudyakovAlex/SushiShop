﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SushiShop.Core.Data.Models;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Abstract;
using SushiShop.Core.ViewModels.Cities.Items;

namespace SushiShop.Core.ViewModels.Cities
{
    public class SelectCityViewModel : SelectablePageItemsViewModel<CityItemViewModel, SelectCityNavigationParameters, int>
    {
        public override string QueryPlaceholder => AppStrings.SearchSity;

        protected override Task<List<CityItemViewModel>> LoadDataAsync(List<int> selectedItemId)
        {
            //TODO: implement API integration
            return Task.FromResult(new List<CityItemViewModel>()
            {
                new CityItemViewModel(new City(1, "Test", 0, 0, 0, "", "")),
                new CityItemViewModel(new City(2, "Test 1", 0, 0, 0, "", ""))
            });
        }
    }
}