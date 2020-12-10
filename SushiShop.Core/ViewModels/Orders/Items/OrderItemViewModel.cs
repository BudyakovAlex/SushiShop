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

        public OrderItemViewModel(Order order, Func<long, Task> repeatOrderFunc)
        {
            this.order = order;
            this.retryOrderFunc = repeatOrderFunc;

            RepeatOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, RepeatOrderAsync);
        }

        public IMvxCommand RepeatOrderCommand { get; }

        private Task RepeatOrderAsync()
        {
            return retryOrderFunc!.Invoke(order.Id);
        }
    }
}