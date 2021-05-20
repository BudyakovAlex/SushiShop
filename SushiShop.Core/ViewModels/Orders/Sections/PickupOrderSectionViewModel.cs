using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class PickupOrderSectionViewModel : BaseOrderSectionViewModel
    {
        private Shop? selectedShop;

        public PickupOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            IDialog dialog,
            Func<OrderConfirmed, string, string, Task> confirmOrderFunc)
            : base(ordersManager, userSession, dialog, confirmOrderFunc)
        {
        }

        public string? ShopAddress => selectedShop?.LongTitle;

        public string? ShopPhone => selectedShop?.Phone;

        public string? ShopTimeWorking => $"{selectedShop?.DeliveryTime}";

        public override string PriceToPay => $"{Cart?.TotalSum - Cart?.Discount - ScoresToApply - DiscountByCard} {Cart?.Currency?.Symbol}";

        protected override DateTime MinDateTimeForPicker => selectedShop is null
            ? base.MinDateTimeForPicker
            : base.MinDateTimeForPicker.AddMinutes(selectedShop.CookingTime + selectedShop.DeliveryTime);

        protected override int MinimumMinutesToReceiveOrder => selectedShop is null ? 0 : selectedShop.CookingTime;

        protected override async Task<OrderConfirmed?> ConfirmOrderAsync()
        {
            var city = UserSession.GetCity();
            var orderRequest = new OrderRequest(
                UserSession.GetCartId(),
                city?.Name,
                Name,
                Phone,
                Comments,
                СutleryStepperViewModel.Count,
                selectedShop?.Id,
                ReceiveDateTime,
                PaymentMethod,
                null,
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
            selectedShop = await NavigationManager.NavigateAsync<ShopsViewModel, bool, Shop>(true);

            await Task.WhenAll(
                RaisePropertyChanged(nameof(ShopAddress)),
                RaisePropertyChanged(nameof(ShopPhone)),
                RaisePropertyChanged(nameof(ReceiveDateTimePresentation)));
        }
    }
}
