using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract.Items;
using SushiShop.Core.Managers.Orders;
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

        public OrderRegistrationViewModel(IOrdersManager ordersManager)
        {
            Items.AddRange(new BaseOrderSectionViewModel[]
            {
                PickupOrderSectionViewModel = new PickupOrderSectionViewModel(ordersManager),
                DeliveryOrderSectionViewModel = new DeliveryOrderSectionViewModel(ordersManager)
            });

            TabsTitles.AddRange(new[] { AppStrings.ReceiveInShop, AppStrings.СourierDelivery });
        }

        public List<string> TabsTitles { get; } = new List<string>();

        public PickupOrderSectionViewModel PickupOrderSectionViewModel { get; }

        public DeliveryOrderSectionViewModel DeliveryOrderSectionViewModel { get; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetProperty(ref _selectedIndex, value);
                IsDelivery = value == 1;
            }
        }

        private bool _isDelivery;
        public bool IsDelivery
        {
            get => _isDelivery;
            set => SetProperty(ref _isDelivery, value);
        }

        public override void Prepare(Data.Models.Cart.Cart parameter)
        {
            cart = parameter;
        }

        public override Task InitializeAsync()
        {
            //TODO: load data here
            return base.InitializeAsync();
        }
    }
}