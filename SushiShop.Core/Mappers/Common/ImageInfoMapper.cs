using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers.Common
{
    public static class ImageInfoMapper
    {
        public static ImageInfo Map(this ImageInfoDto dto) =>
            new ImageInfo(dto.Original!, dto.Jpg!, dto.Webp!);
    }
}