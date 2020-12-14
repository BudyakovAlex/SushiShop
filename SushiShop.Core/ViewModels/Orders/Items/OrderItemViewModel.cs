using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Models.Orders;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class OrderItemViewModel : BaseViewModel
    {
        private readonly Order order;
        private readonly Func<long, Task> repeatOrderFunc;

        public OrderItemViewModel(Order order, Func<long, Task> repeatOrderFunc)
        {
            this.order = order;
            this.repeatOrderFunc = repeatOrderFunc;

            RepeatOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, RepeatOrderAsync);
        }

        public string OrderDateTime => order.OrderDateTime.ToString(Constants.Format.DateTime.DateWithTime);

        public string TotalPrice => $"{order.Total} {order.Currency?.Symbol}";

        public bool CanRepeat => order.CanRepeat;

        public string? Status => order.OrderStateTitle;

        public string? ReceiveMethod => order.ReceiveMethod;

        public string? OrderNumber => $"№{order.Id}";

        public IMvxCommand RepeatOrderCommand { get; }

        private Task RepeatOrderAsync()
        {
            return repeatOrderFunc.Invoke(order.Id);
        }
    }
}