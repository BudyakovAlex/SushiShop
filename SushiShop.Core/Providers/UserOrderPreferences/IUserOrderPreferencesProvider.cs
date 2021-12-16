using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.Providers.UserOrderPreferences
{
    public interface IUserOrderPreferencesProvider
    {
        string? PhoneNumber { get; set; }

        string? UserName { get; set; }

        string? Flat { get; set; }

        string? Section { get; set; }

        string? Floor { get; set; }

        string? Intercom { get; set; }

        void SetAddressSuggestion(AddressSuggestion addressSuggestion);

        AddressSuggestion? GetAddressSuggestion();

        Shop? GetShop();

        void SetShop(Shop shop);

        void ClearAll();
    }
}