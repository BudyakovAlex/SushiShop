using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Providers.UserOrderPreferences;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class DeliveryOrderSectionViewModel : BaseOrderSectionViewModel
    {
        private AddressSuggestion? addressSuggestion;

        public DeliveryOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            IDialog dialog,
            IProfileManager profileManager,
            IUserOrderPreferencesProvider userCredentials,
            ICommand showPrivacyPolicyCommand,
            ICommand showUserAgreementCommand,
            ICommand showPublicOfferCommand,
            Func<OrderConfirmed, string, string, Task> confirmOrderFunc)
            : base(
                  ordersManager,
                  userSession,
                  dialog,
                  profileManager,
                  userCredentials,
                  showPrivacyPolicyCommand,
                  showUserAgreementCommand,
                  showPublicOfferCommand,
                  confirmOrderFunc)
        {
            addressSuggestion = userCredentials.GetAddressSuggestion();
            Flat = userCredentials.Flat;
            Floor = userCredentials.Floor;
            Section = userCredentials.Section;
            Intercom = userCredentials.Intercom;
        }

        public string? DeliveryAddress => addressSuggestion?.FullAddress;

        public string? DeliveryPrice => $"{addressSuggestion?.DeliveryPrice ?? 0} {Cart?.Currency?.Symbol}";

        private string? flat;
        public string? Flat
        {
            get => flat;
            set => SetProperty(ref flat, value);
        }

        private string? section;
        public string? Section
        {
            get => section;
            set => SetProperty(ref section, value);
        }

        private string? floor;
        public string? Floor
        {
            get => floor;
            set => SetProperty(ref floor, value);
        }

        private string? intercom;
        public string? Intercom
        {
            get => intercom;
            set => SetProperty(ref intercom, value);
        }

        protected override DateTime MinDateTimeForPicker => addressSuggestion is null
            ? base.MinDateTimeForPicker
            : base.MinDateTimeForPicker.AddMinutes(addressSuggestion.CookingTime + addressSuggestion.DeliveryTime);

        protected override int MinimumMinutesToReceiveOrder => addressSuggestion is null ? 0 : addressSuggestion.DeliveryTime;

        public override string PriceToPay => $"{(addressSuggestion?.DeliveryPrice ?? 0) + Cart?.TotalSum - Cart?.Discount - ScoresToApply - DiscountByCard} {Cart?.Currency?.Symbol}";

        public override void Prepare(Data.Models.Cart.Cart cart)
        {
            base.Prepare(cart);

            RaisePropertyChanged(nameof(ScoresDiscount));
        }

        protected override async Task<OrderConfirmed?> ConfirmOrderAsync()
        {
            var deliveryRequest = new OrderDeliveryRequest(addressSuggestion?.Address, addressSuggestion?.Coordinates)
            {
                Flat = Flat,
                Floor = Floor,
                Section = Section,
                IntercomCode = Intercom
            };

            var city = UserSession.GetCity();
            var orderRequest = new OrderRequest(
                UserSession.GetCartId(),
                city?.Name,
                Name,
                Phone,
                Comments,
                СutleryStepperViewModel.Count,
                addressSuggestion?.ShopId ?? 0,
                ReceiveDateTime,
                PaymentMethod,
                deliveryRequest,
                0,
                ShouldCallMeBack,
                ShouldApplyScores,
                ScoresToApply);

            var response = await OrdersManager.CreateOrderAsync(orderRequest);
            if (response.Data is null)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return null;
                }

                await Dialog.ShowToastAsync(error);
                return null;
            }

            return response.Data;
        }

        protected override async Task SelectAddressAsync()
        {
            addressSuggestion = await NavigationManager.NavigateAsync<SelectOrderDeliveryAddressViewModel, AddressSuggestion?, AddressSuggestion>(addressSuggestion);
            if (addressSuggestion != null)
            {
                UserOrderPreferences.SetAddressSuggestion(addressSuggestion);
            }

            await Task.WhenAll(
                RaisePropertyChanged(nameof(DeliveryAddress)),
                RaisePropertyChanged(nameof(DeliveryPrice)),
                RaisePropertyChanged(nameof(PriceToPay)),
                RaisePropertyChanged(nameof(ReceiveDateTimePresentation)));
        }

        public override void SaveData()
        {
            base.SaveData();

            UserOrderPreferences.Flat = Flat;
            UserOrderPreferences.Floor = Floor;
            UserOrderPreferences.Intercom = Intercom;
            UserOrderPreferences.Section = Section;
        }
    }
}