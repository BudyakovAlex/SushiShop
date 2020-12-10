using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Messages;
using SushiShop.Core.Providers;
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
        }

        public IMvxCommand RepeatOrderCommand { get; }

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
    }
}