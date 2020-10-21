using System.Collections.Generic;
using SushiShop.Core.Data.Dtos.Stickers;

namespace SushiShop.Core.Data.Dtos.Menu
{
    public class MenuDto
    {
        public CategoryDto[]? Categories { get; set; }
        public Dictionary<string, StickerDto>? Stickers { get; set; }
    }
}
