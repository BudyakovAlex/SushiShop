using SushiShop.Core.Data.Dtos.Franchise;
using SushiShop.Core.Data.Models.Franchise;

namespace SushiShop.Core.Mappers
{
    public static class FranchiseMapper
    {
        public static Franchise Map(this FranchiseDto dto)
        {
            return new Franchise(dto.Title, dto.Url);
        }
    }
}
