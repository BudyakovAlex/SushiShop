using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models
{
    public class Sticker
    {
        public Sticker(StickerType type, int count)
        {
            Type = type;
            Count = count;
        }

        public StickerType Type { get; }

        public int Count { get; }
    }
}
