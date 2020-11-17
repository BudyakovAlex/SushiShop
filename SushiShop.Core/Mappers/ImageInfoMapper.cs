using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers
{
    public static class ImageInfoMapper
    {
        public static ImageInfo Map(this ImageInfoUriDto dto) =>
            new ImageInfo(dto.Original!, dto.Jpg!, dto.Webp!);
    }
}
