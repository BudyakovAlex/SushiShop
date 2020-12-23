using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class OrderDeliveryMapper
    {
        public static OrderDelivery Map(this OrderDeliveryDto dto)
        {
            return new OrderDelivery(dto.Address, dto.Coordinates?.Map());
        }
    }
}
