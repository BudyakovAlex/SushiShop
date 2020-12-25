namespace SushiShop.Core.Data.Models.Common
{
    public class ImageInfo
    {
        public ImageInfo(string? originalUrl, string? jpgUrl, string? webpUrl)
        {
            OriginalUrl = originalUrl;
            JpgUrl = jpgUrl;
            WebpUrl = webpUrl;
        }

        public string? OriginalUrl { get; }

        public string? JpgUrl { get; }

        public string? WebpUrl { get; }
    }
}