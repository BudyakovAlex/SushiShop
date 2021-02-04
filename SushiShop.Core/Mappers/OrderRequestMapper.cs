using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class OrderRequestMapper
    {
        public static OrderRequestDto Map(this OrderRequest model)
        {
            return new OrderRequestDto
            {
                BasketId = model.BasketId,
                BonusesCount = model.BonusesCount,
                CashbackFrom = model.CashbackFrom,
                City = model.City,
                ClientName = model.ClientName,
                Comment = model.Comment,
                Cutlery = model.Cutlery,
                OrderDelivery = model.OrderDelivery?.Map(),
                PaymentMethod = model.PaymentMethod.ToString().ToLower(),
                PhoneNumber = model.PhoneNumber,
                PreferedDateTime = model.PreferedDateTime,
                ShopId = model.ShopId,
                ShouldCallMeBack = model.ShouldCallMeBack,
                ShouldUseBonuses = model.ShouldUseBonuses
            };
        }
    }
}
