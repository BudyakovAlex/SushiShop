using BuildApps.Core.Mobile.MvvmCross.ViewModels.Simple;
using SushiShop.Core.Data.Models;

namespace SushiShop.Core.ViewModels.Cities.Items
{
    public class CityItemViewModel : SelectableItemViewModel<int>
    {
        public CityItemViewModel(City city) : base(city.Name, city.Id)
        {
            City = city;
        }

        public City City { get; }
    }
}