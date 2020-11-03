using System.Drawing;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models.Stickers
{
    public class StickerParams
    {
        public StickerParams(StickerType type, string imageUrl, Color backgroundColor)
        {
            Type = type;
            ImageUrl = imageUrl;
            BackgroundColor = backgroundColor;
        }

        public StickerType Type { get; }
        public string ImageUrl { get; }
        public Color BackgroundColor { get; }
    }
}
