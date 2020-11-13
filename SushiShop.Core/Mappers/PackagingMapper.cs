using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Mappers
{
    public static class PackagingMapper
    {
        public static Packaging Map(this PackagingDto packagingDto)
        {
            return new Packaging(
                packagingDto.Id, packagingDto.PageTitle, packagingDto.Alias,
                packagingDto.Parent, packagingDto.PublishDate, packagingDto.UnpublishDate,
                packagingDto.IntroText, packagingDto.CreationDate, packagingDto.CountInBasket,
                packagingDto.Price, packagingDto.OldPrice, packagingDto.Currency,
                packagingDto.MainImageInfo, packagingDto.OptionalImageInfo);
        }
    }
}
