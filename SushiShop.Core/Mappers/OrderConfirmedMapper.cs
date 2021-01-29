using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class OrderConfirmedMapper
    {
        public static OrderConfirmed Map(this OrderConfirmedDto dto)
        {
            return new OrderConfirmed(dto.OrderNumber, dto.ConfirmationInfo!.Map());
        }
    }
}