﻿using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.Providers;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Orders.Sections;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders
{
    public class OrderRegistrationViewModel : BaseItemsPageViewModel<BaseOrderSectionViewModel, Data.Models.Cart.Cart>
    {
        private Data.Models.Cart.Cart? cart;

        public OrderRegistrationViewModel(IOrdersManager ordersManager, IUserSession userSession)
        {
            Items.AddRange(new BaseOrderSectionViewModel[]
            {
                PickupOrderSectionViewModel = new PickupOrderSectionViewModel(ordersManager, userSession, OrderConfirmedAsync),
                DeliveryOrderSectionViewModel = new DeliveryOrderSectionViewModel(ordersManager, userSession, OrderConfirmedAsync)
            });

            TabsTitles.AddRange(new[] { AppStrings.ReceiveInShop, AppStrings.СourierDelivery });
        }

        public List<string> TabsTitles { get; } = new List<string>();

        public OrderThanksSectionViewModel? OrderThanksSectionViewModel { get; set; }

        public PickupOrderSectionViewModel PickupOrderSectionViewModel { get; }

        public DeliveryOrderSectionViewModel DeliveryOrderSectionViewModel { get; }

        public override void Prepare(Data.Models.Cart.Cart parameter)
        {
            cart = parameter;
        }

        public override Task InitializeAsync()
        {
            //TODO: load data here
            return base.InitializeAsync();
        }

        private async Task OrderConfirmedAsync(OrderConfirmed orderConfirmed)
        {
            OrderThanksSectionViewModel = new OrderThanksSectionViewModel(
                orderConfirmed.ConfirmationInfo,
                orderConfirmed.OrderNumber,
                GoToRootAsync);

            if (orderConfirmed.ConfirmationInfo.PaymentUrl.IsNotNullNorEmpty())
            {
                await Xamarin.Essentials.Browser.OpenAsync(orderConfirmed.ConfirmationInfo.PaymentUrl);
            }

            await RaisePropertyChanged(nameof(OrderThanksSectionViewModel));
        }

        private Task GoToRootAsync()
        {
            return Task.CompletedTask;
        }
    }
}