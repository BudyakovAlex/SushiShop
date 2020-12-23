using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Mappers
{
    public static class PickupPointMapper
    {
        public static PickupPoint Map(this PickupPointDto dto)
        {
            return new PickupPoint(
                dto.Address,
                dto.Phones,
                dto.WorktimeState,
                dto.Worktime,
                dto.Coordinates?.Map());
        }
    }
}