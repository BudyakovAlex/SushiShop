using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Orders;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderDetailsViewModel : BasePageViewModelResult<bool>
    {
        private readonly IOrdersManager ordersManager;

        public OrderDetailsViewModel(IOrdersManager ordersManager)
        {
            RetryOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, RetryOrderAsync);
            this.ordersManager = ordersManager;
        }

        public IMvxCommand RetryOrderCommand { get; }

        private Task RetryOrderAsync()
        {
            ordersManager.re
        }
    }
}