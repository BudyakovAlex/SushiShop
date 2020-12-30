namespace SushiShop.Core.Data.Models.Common
{
    public class SocialNetwork
    {
        public SocialNetwork(string url, string imageUrl)
        {
            Url = url;
            ImageUrl = imageUrl;
        }

        public string Url { get; }

        public string ImageUrl { get; }
    }
}