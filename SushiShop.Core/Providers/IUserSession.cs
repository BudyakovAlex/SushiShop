using SushiShop.Core.Data.Models.Cities;

namespace SushiShop.Core.Providers
{
    public interface IUserSession
    {
        City? GetCity();

        void SetCity(City city);
    }
}
