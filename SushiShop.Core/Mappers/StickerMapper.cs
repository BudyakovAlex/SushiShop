using System;
using System.Collections.Generic;
using System.Linq;
using SushiShop.Core.Data.Dtos.Stickers;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Stickers;

namespace SushiShop.Core.Mappers
{
    public static class StickerMapper
    {
        public static Sticker Map(this StickerDto dto, string stickerType) =>
            new Sticker(
                Enum.Parse<StickerType>(stickerType, ignoreCase: true),
                dto.Title!,
                dto.ItemsCount,
                dto.Image!);

        public static Sticker[] Map(this Dictionary<string, StickerDto> stickers)
        {
            return stickers.Select(kv => kv.Value.Map(kv.Key)).ToArray();
        }
    }
}
