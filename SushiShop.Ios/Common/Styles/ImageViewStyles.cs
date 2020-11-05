using FFImageLoading.Cross;

namespace SushiShop.Ios.Common.Styles
{
    public static class ImageViewStyles
    {
        public static void SetPlaceholders(this MvxCachedImageView imageView, string imageName = ImageNames.DefaultPlaceholder)
        {
            imageView.ErrorPlaceholderImagePath = imageName;
            imageView.LoadingPlaceholderImagePath = imageName;
        }
    }
}
