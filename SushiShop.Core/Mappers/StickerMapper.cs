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
        public static Sticker Map(this StickerDto dto) =>
            new Sticker(
                Enum.Parse<StickerType>(dto.Value, ignoreCase: true),
                dto.ItemsCount);

        public static Sticker[] Map(this Dictionary<string, StickerDto> stickers) =>
            stickers.Values.Select(dto => dto.Map()).ToArray();
    }
}
