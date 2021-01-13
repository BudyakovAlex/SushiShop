using SushiShop.Core.Data.Models.Shops;
using SushiShop.Core.Managers.Orders;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class PickupOrderSectionViewModel : BaseOrderSectionViewModel
    {
        private Shop? selectedShop;

        public PickupOrderSectionViewModel(IOrdersManager ordersManager) : base(ordersManager)
        {
        }

        public string? ShopAddress => selectedShop?.LongTitle;

        public string? ShopPhone => selectedShop?.Phone;

        private string? flat;
        public string? Flat
        {
            get => flat;
            set => SetProperty(ref flat, value);
        }

        private string? entrance;
        public string? Entrance
        {
            get => entrance;
            set => SetProperty(ref entrance, value);
        }

        private string? floor;
        public string? Floor
        {
            get => floor;
            set => SetProperty(ref floor, value);
        }

        private string? intercom;
        public string? Intercom
        {
            get => intercom;
            set => SetProperty(ref intercom, value);
        }

        protected override Task ConfirmOrderAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task SelectAddressAsync()
        {
            selectedShop = await NavigationManager.NavigateAsync<ShopsViewModel, bool, Shop>(true);

            await Task.WhenAll(RaisePropertyChanged(ShopAddress), RaisePropertyChanged(ShopPhone));
        }
    }
}
