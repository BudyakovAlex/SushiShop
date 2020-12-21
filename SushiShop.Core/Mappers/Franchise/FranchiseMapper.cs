using SushiShop.Core.Data.Dtos.Franchise;
using Model = SushiShop.Core.Data.Models.Franchise;

namespace SushiShop.Core.Mappers.Franchise
{
    public static class FranchiseMapper
    {
        public static Model.Franchise Map(this FranchiseDto dto)
        {
            return new Model.Franchise(dto.Title, dto.Url);
        }
    }
}