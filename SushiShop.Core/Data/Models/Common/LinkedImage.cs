namespace SushiShop.Core.Data.Models.Common
{
    public class LinkedImage
    {
        public LinkedImage(string url, string imageUrl)
        {
            Url = url;
            ImageUrl = imageUrl;
        }

        public string Url { get; }

        public string ImageUrl { get; }
    }
}