using SushiShop.Core.Managers.Orders;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class PickupOrderSectionViewModel : BaseOrderSectionViewModel
    {
        public PickupOrderSectionViewModel(IOrdersManager ordersManager) : base(ordersManager)
        {
        }

        protected override Task ConfirmOrderAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task SelectAdressAsync()
        {
            throw new NotImplementedException();
        }
    }
}
