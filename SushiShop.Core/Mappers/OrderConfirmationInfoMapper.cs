using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class OrderConfirmationInfoMapper
    {
        public static OrderConfirmationInfo Map(this OrderConfirmationInfoDto dto)
        {
            return new OrderConfirmationInfo(
                dto.Image,
                dto.Title,
                dto.Content,
                dto.OrderNumberTitle,
                dto.PaymentUrl);
        }
    }
}