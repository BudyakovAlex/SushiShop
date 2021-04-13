using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Messages;
using SushiShop.Core.NavigationParameters;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using SushiShop.Core.ViewModels.Payment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderRegistrationViewModel : BaseItemsPageViewModel<BaseOrderSectionViewModel, Data.Models.Cart.Cart, bool>
    {
        private readonly IOrdersManager ordersManager;
        private readonly IProfileManager profileManager;
        private readonly IUserSession userSession;
        private readonly IDialog dialog;

        private bool isChanged;

        public OrderRegistrationViewModel(
            IOrdersManager ordersManager,
            IProfileManager profileManager,
            IUserSession userSession,
            IDialog dialog)
        {
            this.ordersManager = ordersManager;
            this.profileManager = profileManager;
            this.userSession = userSession;
            this.dialog = dialog;

            Items.AddRange(new BaseOrderSectionViewModel[]
            {
                PickupOrderSectionViewModel = new PickupOrderSectionViewModel(ordersManager, userSession, dialog, OrderConfirmedAsync),
                DeliveryOrderSectionViewModel = new DeliveryOrderSectionViewModel(ordersManager, userSession, dialog, OrderConfirmedAsync)
            });

            TabsTitles.AddRange(new[] { AppStrings.ReceiveInShop, AppStrings.СourierDelivery });

            ShowPrivacyPolicyCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPrivacyPolicyAsync);
            ShowUserAgreementCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowUserAgreementAsync);
            ShowPublicOfferCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowPublicOfferAsync);
        }

        protected override bool DefaultResult => isChanged;

        public List<string> TabsTitles { get; } = new List<string>();

        public OrderThanksSectionViewModel? OrderThanksSectionViewModel { get; set; }

        public PickupOrderSectionViewModel PickupOrderSectionViewModel { get; }

        public DeliveryOrderSectionViewModel DeliveryOrderSectionViewModel { get; }

        public ICommand ShowPrivacyPolicyCommand { get; }

        public ICommand ShowUserAgreementCommand { get; }

        public ICommand ShowPublicOfferCommand { get; }

        public override void Prepare(Data.Models.Cart.Cart parameter)
        {
            Items.ForEach(item => item.Prepare(parameter));
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var getDiscountTask = profileManager.GetDiscountAsync();
            var getProfileTask = profileManager.GetProfileAsync();

            await Task.WhenAll(getDiscountTask, getProfileTask);
            
            if (!getDiscountTask.Result.IsSuccessful ||
                !getProfileTask.Result.IsSuccessful)
            {
                return;
            }
                 
            Items.ForEach(item => item.SetProfileInfo(getDiscountTask.Result.Data, getProfileTask.Result.Data));
        }

        private async Task OrderConfirmedAsync(OrderConfirmed orderConfirmed, string phone)
        {
            isChanged = true;

            await ProduceThanksForOrderSectionAsync(orderConfirmed);

            if (orderConfirmed.ConfirmationInfo.PaymentUrl.IsNotNullNorEmpty())
            {
                await PayForOrderAsync(orderConfirmed, phone);
            }
        }

        private async Task PayForOrderAsync(OrderConfirmed orderConfirmed, string phone)
        {
            await NavigationManager.NavigateAsync<PaymentViewModel, string, bool>(orderConfirmed.ConfirmationInfo.PaymentUrl!);
            var response = await ordersManager.CheckOrderPaymentAsync(orderConfirmed.OrderNumber, phone);
            if (response.Errors.Any())
            {
                await dialog.ShowToastAsync(response.Errors.First());
            }
        }

        private Task ProduceThanksForOrderSectionAsync(OrderConfirmed orderConfirmed)
        {
            OrderThanksSectionViewModel = new OrderThanksSectionViewModel(
               orderConfirmed.ConfirmationInfo,
               orderConfirmed.OrderNumber,
               GoToRootAsync);

            return RaisePropertyChanged(nameof(OrderThanksSectionViewModel));
        }

        private Task GoToRootAsync()
        {
            Messenger.Publish(new RefreshCartMessage(this));
            Messenger.Publish(new RefreshProductsMessage(this));
            Messenger.Publish(new OrderCreatedMessage(this));

            return NavigationManager.CloseAsync(this, false);
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
    }
}