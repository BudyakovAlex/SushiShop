using Acr.UserDialogs;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Providers;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Sections.Abstract
{
    public abstract class BaseOrderSectionViewModel : BaseViewModel
    {
        private readonly Func<OrderConfirmed, Task> confirmOrderFunc;

        public BaseOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            Func<OrderConfirmed, Task> confirmOrderFunc)
        {
            OrdersManager = ordersManager;
            UserSession = userSession;
            this.confirmOrderFunc = confirmOrderFunc;

            СutleryStepperViewModel = new StepperViewModel(1, (oldValue, newValue) => Task.CompletedTask);

            ChangePaymentMethodCommand = new MvxCommand<PaymentMethod>((newPayementType) => PaymentMethod = newPayementType);
            ConfirmOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmOrderInternalAsync);
            SelectAddressCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectAddressAsync);
            SelectReceiveDateTime = new SafeAsyncCommand(ExecutionStateWrapper, SelectReceiveDateTimeAsync);
        }

        public IMvxCommand<PaymentMethod> ChangePaymentMethodCommand { get; }

        public ICommand ConfirmOrderCommand { get; }

        public ICommand SelectAddressCommand { get; }

        public ICommand SelectReceiveDateTime { get; }

        private string? name;
        public string? Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string? phone;
        public string? Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string? comments;
        public string? Comments
        {
            get => comments;
            set => SetProperty(ref comments, value);
        }

        private PaymentMethod paymentMethod;
        public PaymentMethod PaymentMethod
        {
            get => paymentMethod;
            private set => SetProperty(ref paymentMethod, value);
        }

        private bool shouldApplyScores;
        public bool ShouldApplyScores
        {
            get => shouldApplyScores;
            set => SetProperty(ref shouldApplyScores, value);
        }

        private decimal scoresToApply;
        public decimal ScoresToApply
        {
            get => scoresToApply;
            set => SetProperty(ref scoresToApply, value);
        }

        private DateTime? receiveDateTime;
        public DateTime? ReceiveDateTime
        {
            get => receiveDateTime;
            set => SetProperty(ref receiveDateTime, value);
        }

        public string? AvailableScores { get; }

        public string? ProductsPrice { get; }

        public string? DeliveryPrice { get; }

        public string? DiscountByPromocode { get; }

        public string? DiscountByScores { get; }

        public string? PriceToPay { get; }

        public StepperViewModel СutleryStepperViewModel { get; }

        protected IOrdersManager OrdersManager { get; }

        protected IUserSession UserSession { get; }

        protected abstract Task<OrderConfirmed?> ConfirmOrderAsync();

        protected abstract Task SelectAddressAsync();

        private async Task SelectReceiveDateTimeAsync()
        {
            var pickerConfig = new DatePromptConfig
            {
                iOSPickerStyle = iOSPickerStyle.Wheels,
                SelectedDate = ReceiveDateTime
            };

            var result = await UserDialogs.Instance.DatePromptAsync(pickerConfig);
            ReceiveDateTime = result?.SelectedDate;
        }

        private async Task ConfirmOrderInternalAsync()
        {
            var orderConfirmed = await ConfirmOrderAsync();
            if (orderConfirmed is null)
            {
                return;
            }

            var confirmTask = confirmOrderFunc?.Invoke(orderConfirmed) ?? Task.CompletedTask;
            await confirmTask;
        }
    }
}