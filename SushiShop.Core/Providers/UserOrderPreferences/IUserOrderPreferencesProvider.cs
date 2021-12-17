using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.Providers.UserOrderPreferences
{
    public interface IUserOrderPreferencesProvider
    {
        void SetPhoneNumber(string? phone, OrderTabType orderTabType);

        void SetUserName(string? name, OrderTabType orderTabType);

        string? GetPhoneNumber(OrderTabType orderTabType);

        string? GetUserName(OrderTabType orderTabType);

        string? Flat { get; set; }

        string? Section { get; set; }

        string? Floor { get; set; }

        string? Intercom { get; set; }

        void SetAddressSuggestion(AddressSuggestion? addressSuggestion);

        AddressSuggestion? GetAddressSuggestion();

        Shop? GetShop();

        void SetShop(Shop? shop);

        void ClearAll();
    }
}