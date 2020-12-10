using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Orders;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderItemViewModel : BaseViewModel
    {
        private readonly Order order;
        private readonly Func<long, Task> retryOrderFunc;

        public OrderItemViewModel(Order order, Func<long, Task> retryOrderFunc)
        {
            this.order = order;
            this.retryOrderFunc = retryOrderFunc;

            RetryOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, RetryOrderAsync);
        }

        public IMvxCommand RetryOrderCommand { get; }

        private Task RetryOrderAsync()
        {
            return retryOrderFunc!.Invoke(order.Id);
        }
    }
}