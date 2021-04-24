namespace SushiShop.Core.NavigationParameters
{
    public class PhotoDetailsNavigationParams
    {
        public PhotoDetailsNavigationParams(string[] photos, string currentPhoto)
        {
            Photos = photos;
            CurrentPhoto = currentPhoto;
        }

        public string[] Photos { get; }

        public string CurrentPhoto { get; }
    }
}