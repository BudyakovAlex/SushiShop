using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Shops;
using Xamarin.Essentials;

namespace SushiShop.Core.Providers.UserOrderPreferences
{
    public class UserOrderPreferencesProvider : IUserOrderPreferencesProvider
    {
        public string? Flat
        {
            get => Preferences.Get(Constants.Preferences.FlatKey, null);
            set => Preferences.Set(Constants.Preferences.FlatKey, value);
        }

        public string? Section
        {
            get => Preferences.Get(Constants.Preferences.SectionKey, null);
            set => Preferences.Set(Constants.Preferences.SectionKey, value);
        }

        public string? Floor
        {
            get => Preferences.Get(Constants.Preferences.FloorKey, null);
            set => Preferences.Set(Constants.Preferences.FloorKey, value);
        }

        public string? Intercom
        {
            get => Preferences.Get(Constants.Preferences.IntercomKey, null);
            set => Preferences.Set(Constants.Preferences.IntercomKey, value);
        }

        public AddressSuggestion? GetAddressSuggestion()
        {
            return GetSerializedData<AddressSuggestion>(Constants.Preferences.AddressSuggestionKey);
        }

        public void SetAddressSuggestion(AddressSuggestion? addressSuggestion)
        {
            SaveDataWithSerialization(addressSuggestion, Constants.Preferences.AddressSuggestionKey);
        }

        public Shop? GetShop()
        {
            return GetSerializedData<Shop>(Constants.Preferences.ShopKey);
        }

        public void SetShop(Shop? shop)
        {
            SaveDataWithSerialization(shop, Constants.Preferences.ShopKey);
        }

        private TObject GetSerializedData<TObject>(string preferencesKey)
        {
            var savedData = Preferences.Get(preferencesKey, null);
            return savedData != null ? Json.Deserialize<TObject>(savedData) : default;
        }

        private void SaveDataWithSerialization(object? data, string preferencesKey)
        {
            var serializedAddressSuggestion = Json.Serialize(data);
            Preferences.Set(preferencesKey, serializedAddressSuggestion);
        }

        public void ClearAll()
        {
            Flat = null;
            Section = null;
            Floor = null;
            Intercom = null;
            SetAddressSuggestion(null);
            SetShop(null);
            SetPhoneNumber(null, OrderTabType.Delivery);
            SetPhoneNumber(null, OrderTabType.Pickup);
            SetUserName(null, OrderTabType.Delivery);
            SetUserName(null, OrderTabType.Pickup);
        }

        public void SetPhoneNumber(string? phone, OrderTabType orderTabType)
        {
            Preferences.Set($"{Constants.Preferences.PhoneNumberKey}{orderTabType}", phone);
        }

        public void SetUserName(string? name, OrderTabType orderTabType)
        {
            Preferences.Set($"{Constants.Preferences.UserNameKey}{orderTabType}", name);
        }

        public string? GetPhoneNumber(OrderTabType orderTabType)
        {
            return Preferences.Get($"{Constants.Preferences.PhoneNumberKey}{orderTabType}", null);
        }

        public string? GetUserName(OrderTabType orderTabType)
        {
            return Preferences.Get($"{Constants.Preferences.UserNameKey}{orderTabType}", null);
        }
    }
}