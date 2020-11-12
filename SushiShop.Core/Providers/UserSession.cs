using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using System;
using Xamarin.Essentials;

namespace SushiShop.Core.Providers
{
    public class UserSession : IUserSession
    {
        public Guid GetCartId()
        {
            var defaultGuid = Guid.NewGuid();
            var defaultGuidString = defaultGuid.ToString();
            var guidString = Preferences.Get(Constants.Cart.PreferencesCartKey, defaultGuidString);
            if (guidString == defaultGuidString)
            {
                SetCartId(defaultGuid);
            }

            return Guid.Parse(guidString);
        }

        public void SetCartId(Guid id)
        {
            Preferences.Set(Constants.Cart.PreferencesCartKey, id.ToString());
        }

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
