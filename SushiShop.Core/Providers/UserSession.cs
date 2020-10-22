using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using Xamarin.Essentials;

namespace SushiShop.Core.Providers
{
    public class UserSession : IUserSession
    {
        public City? GetCity()
        {
            var city = Preferences.Get(Constants.Menu.PreferencesCityKey, default(string));
            if (city.IsNullOrEmpty())
            {
                return null;
            }

            var result = Json.Deserialize<City>(city);
            return result.data;
        }

        public void SetCity(City city)
        {
            var cityJson = Json.Serialize(city);
            Preferences.Set(Constants.Menu.PreferencesCityKey, cityJson);
        }
    }
}
