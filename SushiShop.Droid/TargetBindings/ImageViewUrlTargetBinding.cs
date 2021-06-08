using Android.Widget;
using Bumptech.Glide;
using MvvmCross.Platforms.Android.Binding.Target;

namespace SushiShop.Droid.TargetBindings
{
    public class ImageViewUrlTargetBinding : MvxAndroidTargetBinding<ImageView, string>
    {
        public const string DefaultImageViewUrlTargetBinding = nameof(DefaultImageViewUrlTargetBinding);
        public const string AdaptedImageViewUrlTargetBinding = nameof(AdaptedImageViewUrlTargetBinding);

        private const string UrlSuffixFormat = "/{0}x{1}.jpg";

        private readonly bool isSizeAdapted;

        public ImageViewUrlTargetBinding(ImageView target, bool isSizeAdapted) : base(target)
        {
            this.isSizeAdapted = isSizeAdapted;

            if (isSizeAdapted)
            {
                target.SetScaleType(ImageView.ScaleType.Center);
            }
        }

        protected override void SetValueImpl(ImageView target, string value)
        {
            if (!isSizeAdapted)
            {
                LoadImage(value);
                return;
            }

            Target.Post(() => LoadImage(GetImagePath(value)));
        }

        private void LoadImage(string url)
        {
            try
            {
                if (!isSizeAdapted)
                {
                    Glide.With(Target.Context).Load(url).Into(Target);
                    return;
                }

                Glide.With(Target.Context)
                     .Load(url)
                     .Placeholder(Resource.Drawable.ic_placeholder)
                     .Error(Resource.Drawable.ic_placeholder)
                     .Into(Target);
            }
            catch
            {
            }
        }

        private string GetImagePath(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return string.Empty;
            }

            var index = imageUrl.LastIndexOf('/');
            if (index == -1)
            {
                return imageUrl;
            }

            return imageUrl[..index] + string.Format(UrlSuffixFormat, Target.Width, Target.Height);
        }
    }
}