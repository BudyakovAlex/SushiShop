using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;
using System;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class OrderMapper
    {
        public static Order Map(this OrderDto orderDto)
        {
            return new Order(
                orderDto.Id,
                orderDto.Price,
                orderDto.IsDeliveryNeeded,
                orderDto.DeliveryAmount,
                orderDto.DiscountAmount,
                orderDto.Total,
                orderDto.Comment,
                orderDto.Phone,
                orderDto.Name,
                orderDto.IsPaid,
                orderDto.IsDelivered,
                orderDto.PaidSum,
                DateTimeOffset.FromUnixTimeSeconds(orderDto.PreferredDeliveryTime),
                orderDto.CutleryCount,
                orderDto.WasSendToKeeper,
                orderDto.IsCallbackNeeded,
                orderDto.CashBack,
                orderDto.ShopId,
                orderDto.OrderStateTitle,
                orderDto.OrderStateAlias,
                orderDto.ShopAddress,
                orderDto.PaymentMethodTitle,
                orderDto.Delivery?.Map(),
                orderDto.PickupPoint?.Map(),
                orderDto.Currency?.Map(),
                orderDto!.Products!.Select(product => product.Map()).ToArray(),
                orderDto.PaymentLink,
                orderDto.CanRepeat,
                DateTimeOffset.FromUnixTimeSeconds(orderDto.OrderDateTime),
                orderDto.OrderDateFormated,
                orderDto.ReceiveMethod,
                orderDto.DeliveryAddress,
                orderDto.DeliveryLatitude,
                orderDto.DeliveryLongitude);
        }
    }
}