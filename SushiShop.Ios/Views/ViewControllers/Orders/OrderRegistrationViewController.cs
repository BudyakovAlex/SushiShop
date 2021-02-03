using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Views.Controls;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {
        private UIButton backButton;
        private DoneAccessoryView doneAccessoryView;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = AppStrings.OrderRegistrationTitle;

            scrollableTabsView.IsFixedTabs = true;
            scrollableTabsView.OnTabChangedAfterTapAction = OnTabChangedAfterTap;
            RootScrollView.Delegate = new OrderRegistrationScrollViewDelegate(OnDecelerated);

            AppartmentDeliveryTextField.Placeholder = $"{AppStrings.Apartment}*";
            EntranceDeliveryTextField.Placeholder = $"{AppStrings.Entrance}*";
            IntercomDeliveryTextField.Placeholder = $"{AppStrings.Intercom}*";
            FloorDeliveryTextField.Placeholder = $"{AppStrings.Floor}*";
            UserNamePickUpTextField.Placeholder = UserNameDeliveryTextField.Placeholder = $"{AppStrings.Name}*";
            UserPhonePickUpTextField.Placeholder = UserPhoneDeliveryTextField.Placeholder = $"{AppStrings.Phone}*";
            CommentPickUpTextView.Placeholder = CommentDeliveryTextView.Placeholder = AppStrings.Comment;
            SpendPointsPickUpTextField.Placeholder = SpendPointsDeliveryTextField.Placeholder = AppStrings.Count;

            doneAccessoryView = new DoneAccessoryView(this.View, () => this.View.EndEditing(true));
            UserPhonePickUpTextField.InputAccessoryView = doneAccessoryView;
            SpendPointsPickUpTextField.InputAccessoryView = doneAccessoryView;
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

            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.PlatformCloseCommand);

            bindingSet.Bind(scrollableTabsView).For(v => v.Items).To(vm => vm.TabsTitles);

            bindingSet.Bind(ChooseAddressPickUpView).For(v => v.BindTap()).To(vm => vm.PickupOrderSectionViewModel.SelectAddressCommand);
            bindingSet.Bind(ChooseAddressDeliveryView).For(v => v.BindTap()).To(vm => vm.DeliveryOrderSectionViewModel.SelectAddressCommand);

            bindingSet.Bind(AboutShopPickUpView).For(v => v.BindVisible()).To(vm => vm.PickupOrderSectionViewModel.ShopAddress)
                .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(ShopAddressPickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopAddress);
            bindingSet.Bind(ShopPhonePickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopPhone);
            bindingSet.Bind(ShopTimeWorkingPickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ShopTimeWorking);
            bindingSet.Bind(ChooseTimePickUpView).For(v => v.BindTap()).To(vm => vm.PickupOrderSectionViewModel.SelectReceiveDateTime);
            bindingSet.Bind(ReceiveTimePickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ReceiveDateTimePresentation);
            bindingSet.Bind(AboutAddressDeliveryView).For(v => v.BindVisible()).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryAddress)
                .WithConversion<StringToBoolConverter>();
            bindingSet.Bind(AddressDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryAddress);
            bindingSet.Bind(AddressPriceDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryPrice);
            bindingSet.Bind(AppartmentDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Flat);
            bindingSet.Bind(EntranceDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Section);
            bindingSet.Bind(IntercomDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Intercom);
            bindingSet.Bind(FloorDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Floor);
            bindingSet.Bind(ReceiveTimeDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.ReceiveDateTimePresentation);

            bindingSet.Bind(UserNamePickUpTextField).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.Name).TwoWay();
            bindingSet.Bind(UserPhonePickUpTextField).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.Phone).TwoWay();
            bindingSet.Bind(UserNameDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Name).TwoWay();
            bindingSet.Bind(UserPhoneDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Phone).TwoWay();

            bindingSet.Bind(CountOfCutleryPickUpStepperView).For(v => v.ViewModel).To(vm => vm.PickupOrderSectionViewModel.СutleryStepperViewModel);
            bindingSet.Bind(CommentPickUpTextView).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.Comments).TwoWay();
            bindingSet.Bind(CountOfCutleryDeliveryStepperView).For(v => v.ViewModel).To(vm => vm.DeliveryOrderSectionViewModel.СutleryStepperViewModel);
            bindingSet.Bind(CommentDeliveryTextView).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.Comments).TwoWay();

            bindingSet.Bind(OnPointPaymentPickUpImageView).For(v => v.BindVisible()).To(vm => vm.PickupOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentPickUpImageView).For(v => v.BindVisible()).To(vm => vm.PickupOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.Online);
            bindingSet.Bind(OnPointPaymentPickUpView).For(v => v.BindTap()).To(vm => vm.PickupOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentPickUpView).For(v => v.BindTap()).To(vm => vm.PickupOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.Online);
            bindingSet.Bind(OnPointPaymentDeliveryImageView).For(v => v.BindVisible()).To(vm => vm.DeliveryOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentDeliveryImageView).For(v => v.BindVisible()).To(vm => vm.DeliveryOrderSectionViewModel.PaymentMethod)
                .WithConversion<PaymentMethodToVisibleConverter>(PaymentMethod.Online);
            bindingSet.Bind(OnPointPaymentDeliveryView).For(v => v.BindTap()).To(vm => vm.DeliveryOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.OnPoint);
            bindingSet.Bind(OnlinePaymentDeliveryView).For(v => v.BindTap()).To(vm => vm.DeliveryOrderSectionViewModel.ChangePaymentMethodCommand)
                .CommandParameter(PaymentMethod.Online);

            bindingSet.Bind(ScoresPickUpView).For(v => v.BindVisible()).To(vm => vm.PickupOrderSectionViewModel.CanApplyScores);
            bindingSet.Bind(ScoresDeliveryView).For(v => v.BindVisible()).To(vm => vm.DeliveryOrderSectionViewModel.CanApplyScores);
            bindingSet.Bind(CountOfScoresPickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.AvailableScoresPresentation);
            bindingSet.Bind(CountOfScoresDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.AvailableScoresPresentation);

            bindingSet.Bind(SpendPointPickUpSwitch).For(v => v.BindOn()).To(vm => vm.PickupOrderSectionViewModel.ShouldApplyScores).TwoWay();
            bindingSet.Bind(SpendPointsPickUpTextField.Superview).For(v => v.BindVisible()).To(vm => vm.PickupOrderSectionViewModel.ShouldApplyScores);
            bindingSet.Bind(SpendPointsPickUpTextField).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ScoresToApply)
                .WithConversion<DecimalToStringConverter>()
                .TwoWay();
            bindingSet.Bind(SpendPointDeliverySwitch).For(v => v.BindOn()).To(vm => vm.DeliveryOrderSectionViewModel.ShouldApplyScores).TwoWay();
            bindingSet.Bind(SpendPointsDeliveryTextField.Superview).For(v => v.BindVisible()).To(vm => vm.DeliveryOrderSectionViewModel.ShouldApplyScores);
            bindingSet.Bind(SpendPointsDeliveryTextField).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.ScoresToApply)
                .WithConversion<DecimalToStringConverter>()
                .TwoWay();

            bindingSet.Bind(ProductsPricePickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ProductsPrice);
            bindingSet.Bind(DiscountByPromocodePickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.DiscountByPromocode);
            bindingSet.Bind(ScoresDiscountPickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.ScoresDiscount);
            bindingSet.Bind(PriceToPayPickUpLabel).For(v => v.Text).To(vm => vm.PickupOrderSectionViewModel.PriceToPay);
            bindingSet.Bind(ConfirmOrderPickUpButton).For(v => v.BindTouchUpInside()).To(vm => vm.PickupOrderSectionViewModel.ConfirmOrderCommand);

            bindingSet.Bind(ProductsPriceDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.ProductsPrice);
            bindingSet.Bind(PriceDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.DeliveryPrice);
            bindingSet.Bind(DiscountByPromocodeDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.DiscountByPromocode);
            bindingSet.Bind(ScoresDiscountDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.ScoresDiscount);
            bindingSet.Bind(PriceToPayDeliveryLabel).For(v => v.Text).To(vm => vm.DeliveryOrderSectionViewModel.PriceToPay);
            bindingSet.Bind(ConfirmOrderDeliveryButton).For(v => v.BindTouchUpInside()).To(vm => vm.DeliveryOrderSectionViewModel.ConfirmOrderCommand);

            bindingSet.Apply();
        }

        private void OnTabChangedAfterTap()
        {
            var xContentOffset = scrollableTabsView.SelectedIndex == 0 ? 0 : RootScrollView.Frame.Width;
            RootScrollView.SetContentOffset(new CGPoint(xContentOffset, 0), true);
        }

        private void OnDecelerated()
        {
            scrollableTabsView.SelectedIndex = RootScrollView.ContentOffset.X <= 0 ? 0 : 1;
        }
    }
}