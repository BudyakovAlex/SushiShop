using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models
{
    public class Sticker
    {
        public Sticker(StickerType type, int count, string imageUrl)
        {
            Type = type;
            Count = count;
            ImageUrl = imageUrl;
        }

        public StickerType Type { get; }

        public int Count { get; }

        public string ImageUrl { get; }
    }
}
