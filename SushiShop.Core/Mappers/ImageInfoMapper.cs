using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Mappers
{
    public static class ImageInfoMapper
    {
        public static ImageInfoUri Map(this ImageInfoUriDto dto) =>
            new ImageInfoUri(dto.Original!, dto.Jpg!, dto.Webp!);
    }
}
