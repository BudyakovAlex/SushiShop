using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class DeliveryOrderSectionViewModel : BaseOrderSectionViewModel
    {
        private OrderDeliveryRequest? orderDeliveryRequest;

        public DeliveryOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            Func<OrderConfirmed, Task> confirmOrderFunc)
            : base(ordersManager, userSession, confirmOrderFunc)
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

        protected override async Task<OrderConfirmed?> ConfirmOrderAsync()
        {
            if (orderDeliveryRequest is null)
            {
                return null;
            }

            orderDeliveryRequest.Flat = Flat;
            orderDeliveryRequest.Floor = Floor;
            orderDeliveryRequest.Section = Section;
            orderDeliveryRequest.IntercomCode = Intercom;
            
            var city = UserSession.GetCity();
            var orderRequest = new OrderRequest(
                UserSession.GetCartId(),
                city?.Name,
                Name!,
                Phone!,
                Comments!,
                СutleryStepperViewModel.Count,
                0,
                ReceiveDateTime,
                PaymentMethod,
                orderDeliveryRequest,
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
            orderDeliveryRequest = await NavigationManager.NavigateAsync<SelectOrderDeliveryAddressViewModel, OrderDeliveryRequest>();
        }
    }
}