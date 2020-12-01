using SushiShop.Core.Data.Models.Cities;
using System;

namespace SushiShop.Core.Providers
{
    public interface IUserSession
    {
        City? GetCity();

        void SetCity(City city);

        Guid GetCartId();

        void SetCartId(Guid id);
    }
}
