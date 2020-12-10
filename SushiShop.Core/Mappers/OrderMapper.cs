using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class OrderMapper
    {
        public static Order Map(this OrderDto orderDto)
        {
            return new Order();
        }
    }
}