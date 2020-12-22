using System;
using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Profile;
using Xamarin.Essentials;

namespace SushiShop.Core.Providers
{
    public class UserSession : IUserSession
    {
        private Token? token;

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

        public Token? GetToken()
        {
            if (token is null)
            {
                return null;
            }

            var value = Preferences.Get(Constants.Preferences.TokenKey, null);
            if (value is null)
            {
                return null;
            }

            var newToken = Json.ForceDeserialize<Token>(value);
            token = newToken;

            return newToken;
        }

        public void SetToken(Token token)
        {
            this.token = token;

            var value = Json.Serialize(token);
            Preferences.Set(Constants.Preferences.TokenKey, value);
        }
    }
}
