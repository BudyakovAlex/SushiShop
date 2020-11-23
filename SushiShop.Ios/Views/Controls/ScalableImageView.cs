using System;
using CoreGraphics;
using FFImageLoading.Cross;
using Foundation;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(ScalableImageView))]
    public class ScalableImageView : MvxCachedImageView
    {
        private const string UrlSuffixFormat = "/{0}x{1}.jpg";

        private CGSize size;

        public ScalableImageView()
        {
        }

        public ScalableImageView(CGRect frame)
            : base(frame)
        {
        }

        protected ScalableImageView(IntPtr handle)
            : base(handle)
        {
        }

        private string imageUrl;
        public string ImageUrl
        {
            get => imageUrl;
            set
            {
                imageUrl = value;
                UpdateImagePath();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var newSize = Bounds.Size;
            if (newSize != size)
            {
                size = newSize;
                UpdateImagePath();
            }
        }

        private void UpdateImagePath()
        {
            if (size == default)
            {
                return;
            }

            var url = ImageUrl;
            if (string.IsNullOrEmpty(url))
            {
                ImagePath = string.Empty;
            }
            else
            {
                var index = url.LastIndexOf('/');
                if (index == -1)
                {
                    ImagePath = url;
                }
                else
                {
                    var width = ToPixels(size.Width);
                    var height = ToPixels(size.Height);
                    ImagePath = url[..index] + string.Format(UrlSuffixFormat, width, height);
                }
            }
        }

        private int ToPixels(nfloat value) =>
            (int) (value * UIScreen.MainScreen.Scale);
    }
}
