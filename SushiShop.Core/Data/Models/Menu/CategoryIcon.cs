namespace SushiShop.Core.Data.Models.Menu
{
    public class CategoryIcon
    {
        public CategoryIcon(string originalUrl, string jpgUrl, string webpUrl)
        {
            OriginalUrl = originalUrl;
            JpgUrl = jpgUrl;
            WebpUrl = webpUrl;
        }

        public string OriginalUrl { get; }

        public string JpgUrl { get; }

        public string WebpUrl { get; }
    }
}
