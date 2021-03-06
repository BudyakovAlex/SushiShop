using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderDetailsViewModel : BasePageViewModel<long, bool>
    {
        private readonly IOrdersManager ordersManager;
        private readonly IUserSession userSession;

        private long orderId;
        private Order? order;
        private bool shouldRefresh;

        public OrderDetailsViewModel(IOrdersManager ordersManager, IUserSession userSession)
        {
            this.ordersManager = ordersManager;
            this.userSession = userSession;

            RepeatOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, RepeatOrderAsync);
            ShowOrderCompositionCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowOrderCompositionAsync);
        }

        public string Title => string.Format(AppStrings.OrderTemplate, order?.Id);

        public string? OrderDateTime => order?.OrderDateTime.ToString(Constants.Format.DateTime.DateWithTime);

        public string? PreferredDeliveryTimeTitle => AppStrings.PreferredDeliveryTime;

        public string? PreferredDeliveryTimeValue => order?.PreferredDeliveryTime.ToString(Constants.Format.DateTime.DateWithTime);

        public string? TotalPriceTitle => $"{AppStrings.Total}:";

        public string? TotalPriceValue => $"{order?.Total} {order?.Currency?.Symbol}";

        public bool CanRepeat => order?.CanRepeat ?? false;

        public string? Status => order?.OrderStateTitle;

        public string? ReceiveMethod => order?.ReceiveMethod;

        public string? OrderNumber => $"№{order?.Id}";

        public string? PriceTitle => AppStrings.Products;

        public string? PriceValue => $"{order?.Price} {order?.Currency?.Symbol}";

        public string? DeliveryAddressTitle => AppStrings.Address;

        public string? DeliveryAddress => order?.DeliveryAddress;

        public string? Phones => order?.PickupPoint?.GetPhonesStringPresentation();

        public string? WorkingTime => order?.PickupPoint?.GetWorkingTimeStringPresentation();

        public string ReceiveMethodTitle => AppStrings.ReceiveMethod;

        // TODO: unhardcode
        public string ReceiveMethodValue => "Магазин";

        public string ShowOrderCompositionTitle => AppStrings.OrderComposition;

        public string RepeatOrderTitle => AppStrings.RepeatOrder;

        public string ReceiveTitle => $"{ReceiveMethod}:";

        // TODO: unhardcode
        public string ReceiveValue => "Бесплатно";

        public IMvxCommand RepeatOrderCommand { get; }

        public IMvxCommand ShowOrderCompositionCommand { get; }

        protected override bool DefaultResult => shouldRefresh;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _ = RefreshDataAsync();
        }

        public override void Prepare(long parameter)
        {
            orderId = parameter;
        }

        protected override async Task RefreshDataAsync()
        {
            var response = await ordersManager.GetOrderAsync(orderId);
            if (!response.IsSuccessful)
            {
                return;
            }

            order = response.Data;
        }

        private async Task RepeatOrderAsync()
        {
            if (order is null)
            {
                return;
            }

            var city = userSession.GetCity();
            await ordersManager.RepeatOrderAsync(orderId, city?.Name);
            Messenger.Publish(new RefreshCartMessage(this));
            shouldRefresh = true;

            await RefreshDataAsync();
        }

        private Task ShowOrderCompositionAsync()
        {
            if (order is null)
            {
                return Task.CompletedTask;
            }

            var navigationParameners = new OrderCompositionNavigationParameters(order.Products, order.Currency);
            return NavigationManager.NavigateAsync<OrderCompositionViewModel, OrderCompositionNavigationParameters>(navigationParameners);
        }
    }
}