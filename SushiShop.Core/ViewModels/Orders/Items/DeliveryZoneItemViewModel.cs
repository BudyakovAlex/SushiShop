using SushiShop.Core.Data.Models.Shops;

namespace SushiShop.Core.ViewModels.Orders.Items
{
    public class DeliveryZoneItemViewModel
    {
        public DeliveryZoneItemViewModel(DeliveryZone deliveryZone)
        {
            DeliveryZone = deliveryZone;
        }

        public DeliveryZone DeliveryZone { get; }
    }
}
