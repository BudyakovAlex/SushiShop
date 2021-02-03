using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class DeliveryOrderSectionViewModel : BaseOrderSectionViewModel
    {
        private AddressSuggestion? addressSuggestion;

        public DeliveryOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            IDialog dialog,
            Func<OrderConfirmed, Task> confirmOrderFunc)
            : base(ordersManager, userSession, dialog, confirmOrderFunc)
        {
        }

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

        public override string PriceToPay => $"{addressSuggestion?.DeliveryPrice ?? 0 + Cart?.TotalSum - Cart?.Discount - ScoresToApply} {Cart?.Currency?.Symbol}";

        public string? DeliveryAddress => addressSuggestion?.FullAddress;

        public string? DeliveryPrice => $"{addressSuggestion?.DeliveryPrice ?? 0} {Cart?.Currency?.Symbol}";

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

        public override void Prepare(Data.Models.Cart.Cart cart)
        {
            base.Prepare(cart);

            RaisePropertyChanged(nameof(ScoresDiscount));
        }

        protected override async Task<OrderConfirmed?> ConfirmOrderAsync()
        {
            if (addressSuggestion is null)
            {
                return null;
            }

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
                Name!,
                Phone!,
                Comments!,
                СutleryStepperViewModel.Count,
                addressSuggestion?.ShopId ?? 0,
                ReceiveDateTime,
                PaymentMethod,
                deliveryRequest,
                0,
                true,
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
            addressSuggestion = await NavigationManager.NavigateAsync<SelectOrderDeliveryAddressViewModel, AddressSuggestion>();
            await Task.WhenAll(
                RaisePropertyChanged(nameof(DeliveryAddress)),
                RaisePropertyChanged(nameof(DeliveryPrice)),
                RaisePropertyChanged(nameof(PriceToPay)));
        }
    }
}