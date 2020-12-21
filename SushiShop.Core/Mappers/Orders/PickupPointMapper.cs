using SushiShop.Core.Data.Dtos.Orders;
using SushiShop.Core.Data.Models.Orders;
using SushiShop.Core.Mappers.Common;

namespace SushiShop.Core.Mappers.Orders
{
    public static class PickupPointMapper
    {
        public static PickupPoint Map(this PickupPointDto dto)
        {
            return new PickupPoint(dto.Address,
                                   dto.Phones,
                                   dto.WorktimeState,
                                   dto.Worktime,
                                   dto.Coordinates?.Map());
        }
    }
}