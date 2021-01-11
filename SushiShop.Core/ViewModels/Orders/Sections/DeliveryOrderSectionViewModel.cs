using SushiShop.Core.Managers.Orders;
using SushiShop.Core.ViewModels.Orders.Sections.Abstract;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Orders.Sections
{
    public class DeliveryOrderSectionViewModel : BaseOrderSectionViewModel
    {
        public DeliveryOrderSectionViewModel(IOrdersManager ordersManager) : base(ordersManager)
        {
        }

        protected override Task ConfirmOrderAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override Task SelectAdressAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
