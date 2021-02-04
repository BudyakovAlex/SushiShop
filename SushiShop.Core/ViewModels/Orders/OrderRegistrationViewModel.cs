using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Messages;
using SushiShop.Core.Plugins;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using SushiShop.Core.ViewModels.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderRegistrationViewModel : BaseItemsPageViewModel<BaseOrderSectionViewModel, Data.Models.Cart.Cart>
    {
        private readonly IProfileManager profileManager;
        private readonly IDialog dialog;

        public OrderRegistrationViewModel(
            IOrdersManager ordersManager,
            IProfileManager profileManager,
            IUserSession userSession,
            IDialog dialog)
        {
            this.profileManager = profileManager;
            this.dialog = dialog;

            Items.AddRange(new BaseOrderSectionViewModel[]
            {
                PickupOrderSectionViewModel = new PickupOrderSectionViewModel(ordersManager, userSession, dialog, OrderConfirmedAsync),
                DeliveryOrderSectionViewModel = new DeliveryOrderSectionViewModel(ordersManager, userSession, dialog, OrderConfirmedAsync)
            });

            TabsTitles.AddRange(new[] { AppStrings.ReceiveInShop, AppStrings.СourierDelivery });
        }

        public List<string> TabsTitles { get; } = new List<string>();

        public OrderThanksSectionViewModel? OrderThanksSectionViewModel { get; set; }

        public PickupOrderSectionViewModel PickupOrderSectionViewModel { get; }

        public DeliveryOrderSectionViewModel DeliveryOrderSectionViewModel { get; }

        public ICommand ShowPrivacyPolicyCommand { get; }

        public ICommand ShowUserAgreementCommand { get; }

        public override void Prepare(Data.Models.Cart.Cart parameter)
        {
            Items.ForEach(item => item.Prepare(parameter));
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var response = await profileManager.GetDiscountAsync();
            if (!response.IsSuccessful)
            {
                return;
            }
                 
            Items.ForEach(item => item.SetDiscount(response.Data));
        }

        private Task OrderConfirmedAsync(OrderConfirmed orderConfirmed)
        {
            return orderConfirmed.ConfirmationInfo.PaymentUrl.IsNotNullNorEmpty()
                ? PayForOrderAsync(orderConfirmed)
                : ProduceThanksForOrderSectionAsync(orderConfirmed);
        }

        private async Task PayForOrderAsync(OrderConfirmed orderConfirmed)
        {
            var isPaymentConfirmed = await NavigationManager.NavigateAsync<PaymentViewModel, string>(orderConfirmed.ConfirmationInfo.PaymentUrl!);
            if (!isPaymentConfirmed)
            {
                await dialog.ShowToastAsync(AppStrings.OrderCreationError);
                return;
            }

            await ProduceThanksForOrderSectionAsync(orderConfirmed);
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
            return NavigationManager.CloseAsync(this);
        }
    }
}