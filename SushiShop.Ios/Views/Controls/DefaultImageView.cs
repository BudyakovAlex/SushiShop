using System;
using CoreGraphics;
using FFImageLoading.Cross;
using Foundation;
using SushiShop.Ios.Common;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(DefaultImageView))]
    public class DefaultImageView : MvxCachedImageView
    {
        public DefaultImageView()
        {
            InitializeDefaultProperties();
        }

        public DefaultImageView(IntPtr handle) : base(handle)
        {
            InitializeDefaultProperties();
        }

        public DefaultImageView(CGRect frame) : base(frame)
        {
            InitializeDefaultProperties();
        }

        private void InitializeDefaultProperties()
        {
            ErrorPlaceholderImagePath = ImageNames.DefaultImage;
            LoadingPlaceholderImagePath = ImageNames.DefaultImage;
        }
    }
}
