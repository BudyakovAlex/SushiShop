namespace SushiShop.Core.Data.Models.Franchise
{
    public class Franchise
    {
        public Franchise(string? title, string? url)
        {
            Title = title;
            Url = url;
        }

        public string? Title { get; }

        public string? Url { get; }
    }
}
