using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models.Stickers
{
    public class Sticker
    {
        public Sticker(StickerType type, string title, int count, string imageUrl)
        {
            Type = type;
            Title = title;
            Count = count;
            ImageUrl = imageUrl;
        }

        public string Title { get; }

        public StickerType Type { get; }

        public int Count { get; }

        public string ImageUrl { get; }
    }
}
