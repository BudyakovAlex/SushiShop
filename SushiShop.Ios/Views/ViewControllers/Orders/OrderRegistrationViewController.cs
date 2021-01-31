using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.ViewModels.Orders;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {

        public bool VisiblePickupDetailsView
        {
            get => PickupDetailsView.Hidden;
            set => PickupDetailsView.Hidden = !(value && scrollableTabsView.SelectedIndex == 0);
        }

        public bool VisibleDeliveryDetailsView
        {
            get => DeliveryDetailsView.Hidden;
            set => DeliveryDetailsView.Hidden = !(value && scrollableTabsView.SelectedIndex == 1);
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            scrollableTabsView.IsFixedTabs = true;

            DeliveryAppartmentTextField.Placeholder = "Квартира*";
            DeliveryEntranceTextField.Placeholder = "Подъезд*";
            DeliveryIntercomTextField.Placeholder = "Домофон*";
            DeliveryFloorTextField.Placeholder = "Этаж*";
            UserNameTextField.Placeholder = "Имя*";
            PhoneUserTextField.Placeholder = "Телефон*";
            CommentTextView.Placeholder = "Комментарий";
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(scrollableTabsView).For(v => v.Items).To(vm => vm.TabsTitles);
            bindingSet.Bind(scrollableTabsView).For(v => v.SelectedIndex).To(vm => vm.SelectedIndex);
            bindingSet.Bind(ChoiceAddressView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.SelectAddressCommand);
            bindingSet.Bind(this).For(v => v.VisiblePickupDetailsView).To(vm => vm.PickupOrderSectionViewModel.ShopAddress)
                .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(this).For(v => v.VisibleDeliveryDetailsView).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryAddress)
                .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(UserNameTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Name).TwoWay();
            bindingSet.Bind(PhoneUserTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Phone).TwoWay();
            bindingSet.Bind(CommentTextView).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Comments).TwoWay();
            bindingSet.Bind(SpendPointSwitch).For(v => v.BindOn()).To(vm => vm.AbstractOrderSectionViewModel.ShouldApplyScores).TwoWay();
            bindingSet.Bind(SpendPointsTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.ScoresToApply).TwoWay();
            bindingSet.Bind(CountOfDevicesStepperView).For(v => v.ViewModel).To(vm => vm.AbstractOrderSectionViewModel.СutleryStepperViewModel);
            bindingSet.Bind(ChooseTimeView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.SelectReceiveDateTime);
            bindingSet.Bind(OnPointPaymentImageView).For(v => v.BindVisible()).To(vm => vm.AbstractOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleImageConverter>(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentimageView).For(v => v.BindVisible()).To(vm => vm.AbstractOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleImageConverter>(PaymentMethod.Online);
            bindingSet.Bind(OnPointPaymentView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.Online);

            bindingSet.Apply();
        }
    }
}

