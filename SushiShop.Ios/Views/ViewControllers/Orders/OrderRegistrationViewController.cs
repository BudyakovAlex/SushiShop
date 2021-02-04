using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.Combiners;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.Converters;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Views.Controls;
using System.Linq;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Orders
{
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class OrderRegistrationViewController : BaseViewController<OrderRegistrationViewModel>
    {
        private readonly NSRange privacyPolicyRange = new NSRange(108, 26);
        private readonly NSRange termsOfThePublicOffer = new NSRange(136, 28);
        private readonly NSRange userAgreement = new NSRange(167, 27);
        private const int MainViewTabIndex = 0;

        private UIButton backButton;
        private DoneAccessoryView doneAccessoryView;

        public OrderThanksSectionViewModel OrderThanksSection
        {
            set
            {
                if (value != null)
                {
                    this.NavigationController.SetNavigationBarHidden(true, true);
                    ThanksOrderView.Hidden = false;
                }
                else
                {
                    this.NavigationController.SetNavigationBarHidden(false, true);
                    ThanksOrderView.Hidden = true;
                }
            }
        }

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

            var attributedText = new NSMutableAttributedString(AppStrings.PrivacyPolicyConfirmOrder);
            attributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), privacyPolicyRange);
            attributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), termsOfThePublicOffer);
            attributedText.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), userAgreement);
            PrivacyPolicyPickUpLabel.AttributedText = attributedText;
            PrivacyPolicyDeliveryLabel.AttributedText = attributedText;
            PrivacyPolicyPickUpLabel.AddGestureRecognizer(new UITapGestureRecognizer(TapOnLabel));
            PrivacyPolicyDeliveryLabel.AddGestureRecognizer(new UITapGestureRecognizer(TapOnLabel));

            ThanksOrderGoToRootButton.AddGestureRecognizer(new UITapGestureRecognizer(TapOnConfirmButton));
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

            bindingSet.Bind(backButton).For(v => v.BindTouchUpInside()).To(vm => vm.CloseCommand);
            bindingSet.Bind(LoadingOverlayView).For(v => v.BindVisible()).ByCombining(new MvxOrValueCombiner(),
                                                                                      vm => vm.PickupOrderSectionViewModel.ExecutionStateWrapper.IsBusy,
                                                                                      vm => vm.DeliveryOrderSectionViewModel.ExecutionStateWrapper.IsBusy);

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
            bindingSet.Bind(ChooseTimeDeliveryView).For(v => v.BindTap()).To(vm => vm.DeliveryOrderSectionViewModel.SelectReceiveDateTime);
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

            bindingSet.Bind(ThanksOrderImageView).For(v => v.ImagePath).To(vm => vm.OrderThanksSectionViewModel.Image);
            bindingSet.Bind(ThanksOrderTitleLabel).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.Title);
            bindingSet.Bind(ThanksOrderContentLabel).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.Content);
            bindingSet.Bind(ThanksOrderNumberTitleLabel).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.OrderNumberTitle);
            bindingSet.Bind(ThanksOrderNumberLabel).For(v => v.Text).To(vm => vm.OrderThanksSectionViewModel.OrderNumber);
            bindingSet.Bind(this).For(nameof(OrderThanksSection)).To(vm => vm.OrderThanksSectionViewModel);

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

        private void TapOnLabel(UITapGestureRecognizer gesture)
        {
            var label = (UILabel)gesture.View;
            if (gesture.DidTapAttributedTextInLabel(label, privacyPolicyRange))
            {
                ViewModel?.ShowPrivacyPolicyCommand?.Execute(null);
            }

            if (gesture.DidTapAttributedTextInLabel(label, termsOfThePublicOffer))
            {
                ViewModel?.ShowPublicOfferCommand?.Execute(null);
            }

            if (gesture.DidTapAttributedTextInLabel(label, userAgreement))
            {
                ViewModel?.ShowUserAgreementCommand?.Execute(null);
            }
        }

        private void TapOnConfirmButton()
        {
            if (UIApplication.SharedApplication.Windows.FirstOrDefault()?.RootViewController is MainViewController mainViewController)
            {
                mainViewController.TabIndex = MainViewTabIndex;
            }

            ViewModel?.OrderThanksSectionViewModel?.GoToRootCommand?.Execute(null);
        }
    }
}