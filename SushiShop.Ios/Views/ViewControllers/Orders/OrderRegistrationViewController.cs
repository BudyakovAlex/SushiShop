using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Ios.Common;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {
        private UIButton backButton;
        private DoneAccessoryView doneAccessoryView;

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

            Title = AppStrings.OrderRegistrationTitle;

            scrollableTabsView.IsFixedTabs = true;

            DeliveryAppartmentTextField.Placeholder = $"{AppStrings.Apartment}*";
            DeliveryEntranceTextField.Placeholder = $"{AppStrings.Entrance}*";
            DeliveryIntercomTextField.Placeholder = $"{AppStrings.Intercom}*";
            DeliveryFloorTextField.Placeholder = $"{AppStrings.Floor}*";
            UserNameTextField.Placeholder = $"{AppStrings.Name}*";
            PhoneUserTextField.Placeholder = $"{AppStrings.Phone}*";
            CommentTextView.Placeholder = AppStrings.Comment;
            SpendPointsTextField.Placeholder = AppStrings.Count;

            doneAccessoryView = new DoneAccessoryView(this.View, () => this.View.EndEditing(true));
            PhoneUserTextField.InputAccessoryView = doneAccessoryView;
            SpendPointsTextField.InputAccessoryView = doneAccessoryView;
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            backButton = Components.CreateDefaultBarButton(ImageNames.ArrowBack);
            navigationItem.LeftBarButtonItem = new UIBarButtonItem(backButton);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(scrollableTabsView).For(v => v.Items).To(vm => vm.TabsTitles);
            bindingSet.Bind(scrollableTabsView).For(v => v.SelectedIndex).To(vm => vm.SelectedIndex);
            bindingSet.Bind(ChoiceAddressView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.SelectAddressCommand);
            bindingSet.Bind(this).For(v => v.VisiblePickupDetailsView).To(vm => vm.PickupOrderSectionViewModel.AvailableVisibleInfo);
            bindingSet.Bind(this).For(v => v.VisibleDeliveryDetailsView).To(vm => vm.DeliveryOrderSectionViewModel.AvailableVisibleInfo);
            bindingSet.Bind(UserNameTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Name).TwoWay();
            bindingSet.Bind(PhoneUserTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Phone).TwoWay();
            bindingSet.Bind(CommentTextView).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.Comments).TwoWay();
            bindingSet.Bind(SpendPointSwitch).For(v => v.BindOn()).To(vm => vm.AbstractOrderSectionViewModel.ShouldApplyScores).TwoWay();
            bindingSet.Bind(SpendPointsTextField).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.ScoresToApply)
                .WithConversion<DecimalToStringConverter>()
                .TwoWay();
            bindingSet.Bind(CountOfDevicesStepperView).For(v => v.ViewModel).To(vm => vm.AbstractOrderSectionViewModel.СutleryStepperViewModel);
            bindingSet.Bind(ChooseTimeView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.SelectReceiveDateTime);
            bindingSet.Bind(OnPointPaymentImageView).For(v => v.BindVisible()).To(vm => vm.AbstractOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentimageView).For(v => v.BindVisible()).To(vm => vm.AbstractOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.Online);
            bindingSet.Bind(OnPointPaymentView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentView).For(v => v.BindTap()).To(vm => vm.AbstractOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.Online);
            bindingSet.Bind(AvailableScorsLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.AvailableScores);
            bindingSet.Bind(ProductsPriceLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.ProductsPrice);
            bindingSet.Bind(DiscountByPromocodeLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.DiscountByPromocode);
            bindingSet.Bind(PriceToPayLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.PriceToPay);
            bindingSet.Bind(DeliveryTitleLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.DeliveryTitle);
            bindingSet.Bind(FullDeliveryPriceLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.DeliveryPrice);
            bindingSet.Bind(DeliveryPriceLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.DeliveryPrice);
            bindingSet.Bind(ScoresDiscountLabel).For(v => v.Text).To(vm => vm.AbstractOrderSectionViewModel.ScoresDiscount);
            bindingSet.Bind(DeliveryView).For(v => v.BindVisible()).To(vm => vm.AbstractOrderSectionViewModel.AvailableVisibleInfo);
            bindingSet.Bind(PickUpAddressLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopAddress);
            bindingSet.Bind(PickUpPhoneLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopPhone);
            bindingSet.Bind(PickUpTimeWorkingLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopTimeWorking);
            bindingSet.Bind(DeliveryAddressLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryAddress);
            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);

            bindingSet.Apply();
        }
    }
}