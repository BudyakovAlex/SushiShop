namespace SushiShop.Core.Data.Dtos.Sticker
{
    public class StickersDto
    {
        public StickersDto(StickerDto? hit, StickerDto? hot, StickerDto? @new, StickerDto? vegan)
        {
            Hit = hit;
            Hot = hot;
            New = @new;
            Vegan = vegan;
        }

        public StickerDto? Hit { get; }

        public StickerDto? Hot { get; }

        public StickerDto? New { get; }

        public StickerDto? Vegan { get; }
    }
}
