using System;
using System.Collections.Generic;
using System.Linq;
using SushiShop.Core.Data.Dtos.Sticker;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models;

namespace SushiShop.Core.Mappers
{
    public static class StickerMapper
    {
        public static Sticker Map(this StickerDto dto)
        {
            var type = Enum.TryParse<StickerType>(dto.Value, ignoreCase: true, out var stickerType)
                ? stickerType
                : StickerType.Unknown;

            return new Sticker(type, dto.ItemsCount);
        }

        public static Sticker[] Map(this StickersDto dto)
        {
            return ToEnumerable(dto)
                .Where(x => x != null)
                .Select(x => x!.Map())
                .ToArray();

            static IEnumerable<StickerDto?> ToEnumerable(StickersDto stickers)
            {
                yield return stickers.Hit;
                yield return stickers.Hot;
                yield return stickers.New;
                yield return stickers.Vegan;
            }
        }
    }
}
