using SushiShop.Core.Data.Dtos.Cart;
using SushiShop.Core.Data.Models.Cart;
using System.Linq;

namespace SushiShop.Core.Mappers
{
    public static class SaucesMapper
    {
        public static Sauces Map(this SaucesDto saucesDto)
        {
            return new Sauces(
                saucesDto.Id,
                saucesDto.PageTitle,
                saucesDto.Alias,
                saucesDto.Parent,
                saucesDto.ToppingCategory,
                saucesDto.PublishDate,
                saucesDto.UnpublishDate,
                saucesDto.IntroText,
                saucesDto.CreationDate,
                saucesDto.CountInBasket,
                saucesDto.Price,
                saucesDto.OldPrice,
                saucesDto.Currency?.Select(currency => currency.Map()),
                saucesDto.MainImageInfo?.Select(infoImage => infoImage.Map()),
                saucesDto.OptionalImageInfo?.Select(infoImage => infoImage.Map()));
        }
    }
}
