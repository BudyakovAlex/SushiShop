using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Mappers.Common;

namespace SushiShop.Core.Mappers.Orders
{
    public static class OrderDeliveryMapper
    {
        public static OrderDelivery Map(this OrderDeliveryDto dto)
        {
            return new OrderDelivery(dto.Address,
                                     dto.Coordinates?.Map());
        }
    }
}