namespace SushiShop.Core.Data.Dtos.Sticker
{
    public class StickerDto
    {
        public StickerDto(string value, int itemsCount)
        {
            Value = value;
            ItemsCount = itemsCount;
        }

        public string Value { get; }

        public int ItemsCount { get; }
    }
}
