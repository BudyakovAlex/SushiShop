using Acr.UserDialogs;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Sections.Abstract
{
    public abstract class BaseOrderSectionViewModel : BaseViewModel
    {
        public BaseOrderSectionViewModel(IOrdersManager ordersManager)
        {
            OrdersManager = ordersManager;

            СutleryStepperViewModel = new StepperViewModel(1, (oldValue, newValue) => Task.CompletedTask);

            ChangePaymentTypeCommand = new MvxCommand<PaymentType>((newPayementType) => PaymentType = newPayementType);
            ConfirmOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmOrderAsync);
            SelectAdressCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectAdressAsync);
            SelectReceiveDateTime = new SafeAsyncCommand(ExecutionStateWrapper, SelectReceiveDateTimeAsync);
        }

        public IMvxCommand<PaymentType> ChangePaymentTypeCommand { get; }

        public ICommand ConfirmOrderCommand { get; }

        public ICommand SelectAdressCommand { get; }

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

        private PaymentType paymentType;
        public PaymentType PaymentType
        {
            get => paymentType;
            private set => SetProperty(ref paymentType, value);
        }

        private bool shouldApplyScores;
        public bool ShouldApplyScores
        {
            get => shouldApplyScores;
            set => SetProperty(ref shouldApplyScores, value);
        }

        private string? scoresToApply;
        public string? ScoresToApply
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

        protected abstract Task ConfirmOrderAsync();

        protected abstract Task SelectAdressAsync();

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
    }
}