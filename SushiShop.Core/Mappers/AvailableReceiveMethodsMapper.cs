using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Models.Cities;

namespace SushiShop.Core.Mappers
{
    public static class AvailableReceiveMethodsMapper
    {
        public static AvailableReceiveMethods Map(this AvailableReceiveMethodsDto dto)
        {
            return new AvailableReceiveMethods(
                dto.CanDelivery,
                dto.CanPickup);
        }
    }
}
