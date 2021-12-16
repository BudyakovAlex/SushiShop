using System;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Profile;
using Xamarin.Essentials;

namespace SushiShop.Core.Providers
{
    public class UserSession : IUserSession
    {
        private Token? token;
        private City? city;
        private Guid? cartId;

        public Guid GetCartId()
        {
            if (cartId.HasValue)
            {
                return cartId.Value;
            }

            var json = Preferences.Get(Constants.Preferences.CartKey, null);
            if (json is null)
            {
                var newCartId = Guid.NewGuid();
                SetCartId(newCartId);

                return newCartId;
            }
            else
            {
                var newCartId = Guid.Parse(json);
                cartId = newCartId;

                return newCartId;
            }
        }

        public void SetCartId(Guid id)
        {
            cartId = id;

            Preferences.Set(Constants.Preferences.CartKey, id.ToString());
        }

        public City? GetCity()
        {
            if (city != null)
            {
                return city;
            }

            var json = Preferences.Get(Constants.Preferences.CityKey, null);
            if (json is null)
            {
                return null;
            }

            var newCity = Json.Deserialize<City>(json);
            city = newCity;

            return newCity;
        }

        public void SetCity(City city)
        {
            this.city = city;

            var value = Json.Serialize(city);
            Preferences.Set(Constants.Preferences.CityKey, value);
        }

        public Token? GetToken()
        {
            if (token != null)
            {
                return token;
            }

            var json = Preferences.Get(Constants.Preferences.TokenKey, null);
            if (json is null)
            {
                return null;
            }

            var newToken = Json.Deserialize<Token>(json);
            token = newToken;

            return newToken;
        }

        public void SetToken(Token? token)
        {
            this.token = token;

            if (token is null)
            {
                Preferences.Set(Constants.Preferences.TokenKey, null);
            }
            else
            {
                var json = Json.Serialize(token);
                Preferences.Set(Constants.Preferences.TokenKey, json);
            }
        }

        public bool CheckIsValidToken()
        {
            return token != null && token.ExpiresAt > DateTime.Now;
        }
    }
}
