using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using Plugin.StoreReview;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Cities;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Data.Models.Profile;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Cities;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Providers.UserOrderPreferences;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using SushiShop.Core.ViewModels.Payment;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderRegistrationViewModel : BaseItemsPageViewModel<BaseOrderSectionViewModel, OrderRegistrationNavigationParameter, bool>
    {
        private readonly IOrdersManager ordersManager;
        private readonly IProfileManager profileManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;
        private readonly IUserOrderPreferencesProvider userOrderPreferences;
        private readonly ICitiesManager citiesManager;

        private DetailedProfile? profile;
        private bool isChanged;

        public OrderRegistrationViewModel(
            IOrdersManager ordersManager,
            IProfileManager profileManager,
            IUserSession userSession,
            IDialog dialog,
            IUserOrderPreferencesProvider userOrderPreferences,
            ICitiesManager citiesManager)
        {
            this.ordersManager = ordersManager;
            this.profileManager = profileManager;
            this.userSession = userSession;
            this.dialog = dialog;
            this.userOrderPreferences = userOrderPreferences;
            this.citiesManager = citiesManager;

            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
            ShowUserAgreementCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowUserAgreementAsync);
            ShowPublicOfferCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPublicOfferAsync);

            TabsTitles = new List<string>();
        }

        protected override bool DefaultResult => isChanged;

        public List<string> TabsTitles { get; }

        public OrderThanksSectionViewModel? OrderThanksSectionViewModel { get; set; }

        public PickupOrderSectionViewModel? PickupOrderSectionViewModel { get; private set; }

        public DeliveryOrderSectionViewModel? DeliveryOrderSectionViewModel { get; private set; }

        private int selectedTab;
        public int SelectedTab
        {
            get => selectedTab;
            set => SetProperty(ref selectedTab, value);
        }

        public ICommand ShowPrivacyPolicyCommand { get; }

        public ICommand ShowUserAgreementCommand { get; }

        public ICommand ShowPublicOfferCommand { get; }

        public override void Prepare(OrderRegistrationNavigationParameter parameter)
        {
            SetAvailableTabs(parameter.AvailableReceiveMethods);
            Items.ForEach(item => item.Prepare(parameter.Cart));
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getDiscountTask = profileManager.GetDiscountAsync();
            var getProfileTask = profileManager.GetProfileAsync();

            await Task.WhenAll(getDiscountTask, getProfileTask);

            var addressSuggestion = userOrderPreferences.GetAddressSuggestion();
            if (addressSuggestion != null)
            {
                var getSearchSuggestionAddress = await citiesManager.SearchByLocationAsync(addressSuggestion.Coordinates!, CancellationToken.None);
                var item = getSearchSuggestionAddress?.Data?.FirstOrDefault(item => item.FiasId == addressSuggestion.FiasId && item.KladrId == addressSuggestion.KladrId);
                userOrderPreferences.SetAddressSuggestion(item);
                DeliveryOrderSectionViewModel!.UpdateSuggesstionAddress();
            }

            if (!getDiscountTask.Result.IsSuccessful ||
                !getProfileTask.Result.IsSuccessful)
            {
                return;
            }

            profile = getProfileTask.Result.Data;
            Items.ForEach(item => item.SetProfileInfo(getDiscountTask.Result.Data, getProfileTask.Result.Data));
        }

        protected override Task CloseAsync(bool? isPlatform)
        {
            Items.ForEach(item => item.SaveData());
            return base.CloseAsync(isPlatform);
        }

        private async Task OrderConfirmedAsync(
            OrderConfirmed orderConfirmed,
            string phone,
            string username)
        {
            isChanged = true;

            await ProduceThanksForOrderSectionAsync(orderConfirmed, phone);

            if (orderConfirmed.ConfirmationInfo.PaymentUrl.IsNotNullNorEmpty())
            {
                await PayForOrderAsync(orderConfirmed, phone);
            }

            await ShowRateUsRequestIfNeededAsync(phone, username);
        }

        private async Task PayForOrderAsync(OrderConfirmed orderConfirmed, string phone)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                await Browser.OpenAsync(orderConfirmed.ConfirmationInfo.PaymentUrl, BrowserLaunchMode.SystemPreferred);
            }
            else
            {
                await NavigationManager.NavigateAsync<PaymentViewModel, string, bool>(orderConfirmed.ConfirmationInfo.PaymentUrl!);
                var response = await ordersManager.CheckOrderPaymentAsync(orderConfirmed.OrderNumber, phone);
                if (response.Errors.Any())
                {
                    await dialog.ShowToastAsync(response.Errors.First());
                }
            }
        }

        private Task ProduceThanksForOrderSectionAsync(OrderConfirmed orderConfirmed, string phone)
        {
            OrderThanksSectionViewModel = new OrderThanksSectionViewModel(
               orderConfirmed.ConfirmationInfo,
               orderConfirmed.OrderNumber,
               phone,
               GoToRootAsync);

            return RaisePropertyChanged(nameof(OrderThanksSectionViewModel));
        }

        private async void GoToRootAsync(long orderNumber, string phone)
        {
            Messenger.Publish(new RefreshCartMessage(this));
            Messenger.Publish(new RefreshProductsMessage(this));
            Messenger.Publish(new OrderCreatedMessage(this));

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var response = await ordersManager.CheckOrderPaymentAsync(orderNumber, phone);
                if (response.Errors.Any())
                {
                    await dialog.ShowToastAsync(response.Errors.First());
                }
                else
                {
                    await NavigationManager.CloseAsync(this, false);
                }
            }
            else
            {
                await NavigationManager.CloseAsync(this, false);
            }
        }

        private Task ShowPrivacyPolicyAsync()
        {
            var city = userSession.GetCity();
            var navigationParameters = new CommonInfoNavigationParameters(CommonInfoType.Content, 0, city?.Name, Constants.Rest.PrivacyPolicyResource, AppStrings.PrivacyPolicy);
            return NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(navigationParameters);
        }

        private Task ShowUserAgreementAsync()
        {
            var city = userSession.GetCity();
            var navigationParameters = new CommonInfoNavigationParameters(CommonInfoType.Content, 0, city?.Name, Constants.Rest.UserAgreementResource, AppStrings.UserAgreement);
            return NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(navigationParameters);
        }

        private Task ShowPublicOfferAsync()
        {
            var city = userSession.GetCity();
            var navigationParameters = new CommonInfoNavigationParameters(CommonInfoType.Content, 0, city?.Name, Constants.Rest.PublicOfferResource, AppStrings.PublicOffer);
            return NavigationManager.NavigateAsync<CommonInfoViewModel, CommonInfoNavigationParameters>(navigationParameters);
        }

        private async Task ShowRateUsRequestIfNeededAsync(string phone, string username)
        {
            var canShowRatePopup = Preferences.Get(Constants.Preferences.RateUsKey, true);
            if (!canShowRatePopup)
            {
                return;
            }

            Preferences.Set(Constants.Preferences.RateUsKey, false);

            //HACK: to avoid design issue
            var isRateFlowCancelled = await UserDialogs.Instance.ConfirmAsync(
                string.Empty,
                AppStrings.DoYouLikeSushiShopApp,
                okText: AppStrings.No,
                cancelText: AppStrings.Yes);
            if (isRateFlowCancelled)
            {
                var actualUsername = profile?.FirstName ?? username;
                var actualPhone = profile?.Phone ?? phone;

                var feedbackSubject = $"{AppStrings.FeedbackFrom}, {DeviceInfo.Platform}, {actualUsername}, {actualPhone}";
                await Email.ComposeAsync(feedbackSubject, string.Empty, Constants.Info.FeedbackEmail);
                return;
            }

            //TODO: remove test mode after testflight
            await CrossStoreReview.Current.RequestReview(false);
        }

        private void SetAvailableTabs(AvailableReceiveMethods availableReceiveMethods)
        {
            if (availableReceiveMethods.CanPickup)
            {
                PickupOrderSectionViewModel = new PickupOrderSectionViewModel(
                    ordersManager,
                    userSession,
                    dialog,
                    profileManager,
                    userOrderPreferences,
                    ShowPrivacyPolicyCommand,
                    ShowUserAgreementCommand,
                    ShowPublicOfferCommand,
                    OrderConfirmedAsync);
                Items.Add(PickupOrderSectionViewModel);
                TabsTitles.Add(AppStrings.ReceiveInShop);
            }

            if (availableReceiveMethods.CanDelivery)
            {
                DeliveryOrderSectionViewModel = new DeliveryOrderSectionViewModel(
                    ordersManager,
                    userSession,
                    dialog,
                    profileManager,
                    userOrderPreferences,
                    ShowPrivacyPolicyCommand,
                    ShowUserAgreementCommand,
                    ShowPublicOfferCommand,
                    OrderConfirmedAsync);
                Items.Add(DeliveryOrderSectionViewModel);
                TabsTitles.Add(AppStrings.СourierDelivery);
            }

            RaiseAllPropertiesChanged();
        }
    }
}