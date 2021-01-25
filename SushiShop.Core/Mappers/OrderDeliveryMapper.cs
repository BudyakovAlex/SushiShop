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

        public static OrderDeliveryRequestDto Map(this OrderDeliveryRequest model)
        {
            return new OrderDeliveryRequestDto
            {
                Address = model.Address,
                Floor = model.Floor,
                Flat = model.Flat,
                Coordinates = model.Cordinates?.Map(),
                IntercomCode = model.IntercomCode,
                Section = model.Section
            };
        }
    }
}