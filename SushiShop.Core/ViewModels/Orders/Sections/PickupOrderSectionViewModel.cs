using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Orders;
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
            Func<OrderConfirmed, Task> confirmOrderFunc)
            : base(ordersManager, userSession, confirmOrderFunc)
        {
        }

        public string? ShopAddress => selectedShop?.LongTitle;

        public string? ShopPhone => selectedShop?.Phone;

        public string? ShopTimeWorking => $"{selectedShop?.DeliveryTime}";

        public override string PriceToPay => $"{Cart?.TotalSum - Cart?.Discount - ScoresToApply} {Cart?.Currency?.Symbol}";

        protected override async Task<OrderConfirmed?> ConfirmOrderAsync()
        {
            var city = UserSession.GetCity();
            var orderRequest = new OrderRequest(
                UserSession.GetCartId(),
                city?.Name,
                Name!,
                Phone!,
                Comments!,
                СutleryStepperViewModel.Count,
                selectedShop!.Id,
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

                await UserDialogs.Instance.AlertAsync(error);
                return null;
            }

            return response.Data;
        }

        protected override async Task SelectAddressAsync()
        {
            selectedShop = await NavigationManager.NavigateAsync<ShopsViewModel, bool, Shop>(true);

            await Task.WhenAll(RaisePropertyChanged(ShopAddress), RaisePropertyChanged(ShopPhone));
        }
    }
}
