using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders.Sections.Abstract
{
    public abstract class BaseOrderSectionViewModel : BaseViewModel
    {
        private readonly Func<OrderConfirmed, string, string, Task> confirmOrderFunc;

        public BaseOrderSectionViewModel(
            IOrdersManager ordersManager,
            IUserSession userSession,
            IDialog dialog,
            ICommand showPrivacyPolicyCommand,
            ICommand showUserAgreementCommand,
            ICommand showPublicOfferCommand,
            Func<OrderConfirmed, string, string, Task> confirmOrderFunc)
        {
            OrdersManager = ordersManager;
            UserSession = userSession;
            Dialog = dialog;
            ShowPrivacyPolicyCommand = showPrivacyPolicyCommand;
            ShowUserAgreementCommand = showUserAgreementCommand;
            ShowPublicOfferCommand = showPublicOfferCommand;

            this.confirmOrderFunc = confirmOrderFunc;

            СutleryStepperViewModel = new StepperViewModel(1, (oldValue, newValue) => Task.CompletedTask);

            ChangePaymentMethodCommand = new MvxCommand<PaymentMethod>((newPayementType) => PaymentMethod = newPayementType);
            ConfirmOrderCommand = new SafeAsyncCommand(ExecutionStateWrapper, ConfirmOrderInternalAsync);
            SelectAddressCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectAddressAsync);
            SelectReceiveDateTimeCommand = new SafeAsyncCommand(ExecutionStateWrapper, SelectReceiveDateTimeAsync);
            RefreshDiscountByCartCommand = new SafeAsyncCommand(ExecutionStateWrapper, RefreshDiscountByCartAsync);
        }

        public IMvxCommand<PaymentMethod> ChangePaymentMethodCommand { get; }

        public ICommand ConfirmOrderCommand { get; }

        public ICommand SelectAddressCommand { get; }

        public ICommand SelectReceiveDateTimeCommand { get; }

        public ICommand RefreshDiscountByCartCommand { get; }

        public ICommand ShowPrivacyPolicyCommand { get; }

        public ICommand ShowUserAgreementCommand { get; }

        public ICommand ShowPublicOfferCommand { get; }

        private string? name = string.Empty;
        public string? Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string? phone = string.Empty;
        public string? Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string? comments = string.Empty;
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
            set
            {
                scoresToApply = Math.Min(value, AvailableScores);
                OnScoresToApplyChanged();
            }
        }

        private DateTime? receiveDateTime;
        public DateTime? ReceiveDateTime
        {
            get => receiveDateTime;
            set => SetProperty(ref receiveDateTime, value, () => RaisePropertyChanged(nameof(ReceiveDateTimePresentation)));
        }

        private decimal discountByCard;
        public decimal DiscountByCard
        {
            get => discountByCard;
            set => SetProperty(ref discountByCard, value, OnDiscountByCardChanged);
        }

        public string? AvailableScoresPresentation { get; private set; }

        public string ProductsPrice => $"{Cart?.TotalSum} {Cart?.Currency?.Symbol}";

        public string DiscountByPromocode => Cart?.Discount > 0 ? $"- {Cart?.Discount} {Cart?.Currency?.Symbol}" : $"{Cart?.Discount} {Cart?.Currency?.Symbol}";

        public string? ScoresDiscount => ScoresToApply > 0 ? $"- {ScoresToApply} {Cart?.Currency?.Symbol}" : $"{ScoresToApply} {Cart?.Currency?.Symbol}";

        public string? DiscountByCardPresentation => DiscountByCard > 0 ? $"- {DiscountByCard} {Cart?.Currency?.Symbol}" : $"{DiscountByCard} {Cart?.Currency?.Symbol}";

        public bool CanApplyScores { get; private set; }

        public string? ReceiveDateTimePresentation => GetReceiveTimePresentation();

        public abstract string PriceToPay { get; }

        public StepperViewModel СutleryStepperViewModel { get; }

        protected int AvailableScores { get; private set; }

        protected Data.Models.Cart.Cart? Cart { get; private set; }

        protected virtual DateTime MinDateTimeForPicker => DateTime.Now;

        protected IOrdersManager OrdersManager { get; }

        protected IUserSession UserSession { get; }

        protected IDialog Dialog { get; }

        protected abstract int MinimumMinutesToReceiveOrder { get; }

        public virtual void Prepare(Data.Models.Cart.Cart cart)
        {
            Cart = cart;

            RaisePropertyChanged(nameof(ProductsPrice));
            RaisePropertyChanged(nameof(DiscountByPromocode));
        }

        public void SetProfileInfo(ProfileDiscount? discount, DetailedProfile? detailedProfile)
        {
            SetDiscount(discount);
            SetProfile(detailedProfile);

            _ = SafeExecutionWrapper.WrapAsync(RefreshDiscountByCartAsync);
        }

        protected abstract Task<OrderConfirmed?> ConfirmOrderAsync();

        protected abstract Task SelectAddressAsync();

        private void OnScoresToApplyChanged()
        {
            RaisePropertyChanged(nameof(ScoresToApply));
            RaisePropertyChanged(nameof(ScoresDiscount));
            RaisePropertyChanged(nameof(PriceToPay));
        }

        private void OnDiscountByCardChanged()
        {
            RaisePropertyChanged(nameof(DiscountByCardPresentation));
            RaisePropertyChanged(nameof(PriceToPay));
        }

        private async Task SelectReceiveDateTimeAsync()
        {
            var selectedDate = ReceiveDateTime ?? MinDateTimeForPicker;
            ReceiveDateTime = await Dialog.ShowDatePickerAsync(selectedDate, MinDateTimeForPicker, MinDateTimeForPicker.AddDays(7), DatePickerMode.DateAndTime);
        }

        private async Task RefreshDiscountByCartAsync()
        {
            var response = await OrdersManager.CalculateDiscountAsync(Phone);
            if (!response.IsSuccessful)
            {
                DiscountByCard = 0;
                return;
            }

            DiscountByCard = response.Data;
        }

        private void SetProfile(DetailedProfile? detailedProfile)
        {
            if (detailedProfile is null)
            {
                return;
            }

            Phone = detailedProfile.Phone?.FilterByMask(Constants.Format.Phone.MaskFormat);
            Name = detailedProfile.FirstName;
        }

        private void SetDiscount(ProfileDiscount? discount)
        {
            if (discount?.Bonuses <= 0)
            {
                return;
            }

            CanApplyScores = true;
            AvailableScoresPresentation = $"{discount!.Bonuses} {AppStrings.Scores}";
            var productPriceCanBePayedByScores = (Cart?.TotalSum * 0.3m) ?? discount!.Bonuses;
            AvailableScores = (int)Math.Min(productPriceCanBePayedByScores, discount!.Bonuses);

            RaisePropertyChanged(nameof(AvailableScoresPresentation));
            RaisePropertyChanged(nameof(CanApplyScores));
        }

        private string? GetReceiveTimePresentation()
        {
            if (ReceiveDateTime is null)
            {
                return string.Format(AppStrings.ReceiveTimeTemplate, MinimumMinutesToReceiveOrder);
            }

            return receiveDateTime!.Value.ToString(Constants.Format.DateTime.DateWithTwentyFourTime);
        }

        private async Task ConfirmOrderInternalAsync()
        {
            var orderConfirmed = await ConfirmOrderAsync();
            if (orderConfirmed is null)
            {
                return;
            }

            var confirmTask = confirmOrderFunc?.Invoke(orderConfirmed, Phone!, Name!) ?? Task.CompletedTask;
            await confirmTask;
        }
    }
}