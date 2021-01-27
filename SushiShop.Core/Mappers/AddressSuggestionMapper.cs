using SushiShop.Core.Data.Dtos.Cities;
using SushiShop.Core.Data.Models.Cities;

namespace SushiShop.Core.Mappers
{
    public static class AddressSuggestionMapper
    {
        public static AddressSuggestion Map(this AddressSuggestionDto dto)
        {
            return new AddressSuggestion(
                dto.Address,
                dto.FullAddress,
                dto.FiasId,
                dto.KladrId,
                dto.ZipCode,
                dto.IsHouseAddress,
                dto.Coordinates?.Map());
        }
    }
}