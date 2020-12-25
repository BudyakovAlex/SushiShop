using System;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Providers
{
    public interface IUserSession
    {
        City? GetCity();

        void SetCity(City city);

        Guid GetCartId();

        void SetCartId(Guid id);

        Token? GetToken();

        void SetToken(Token? token);
    }
}
