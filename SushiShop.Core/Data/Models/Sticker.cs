using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models
{
    public class Sticker
    {
        public Sticker(StickerType type, int count, string? image)
        {
            Type = type;
            Count = count;
            Image = image;
        }

        public StickerType Type { get; }

        public int Count { get; }

        public string? Image { get; }
    }
}
