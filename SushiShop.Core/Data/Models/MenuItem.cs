namespace SushiShop.Core.Data.Models
{
    public class MenuItem
    {
        public MenuItem(string title, string imageUri)
        {
            Title = title;
            ImageUri = imageUri;
        }

        public string Title { get; }

        public string ImageUri { get; }
    }
}