using Android.Widget;
using BumpTech.GlideLib;
using MvvmCross.Platforms.Android.Binding.Target;

namespace SushiShop.Droid.TargetBindings
{
    public class ImageViewUrlTargetBinding : MvxAndroidTargetBinding<ImageView, string>
    {
        public const string DefaultImageViewUrlTargetBinding = nameof(DefaultImageViewUrlTargetBinding);
        public const string AdaptedImageViewUrlTargetBinding = nameof(AdaptedImageViewUrlTargetBinding);

        private const string UrlSuffixFormat = "/{0}x{1}.jpg";

        private readonly bool isSizeAdapted;

        public ImageViewUrlTargetBinding(ImageView target, bool isSizeAdapted = false) : base(target)
        {
            this.isSizeAdapted = isSizeAdapted;
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
            var request = Glide.With(Target.Context).Load(url);
            request.Placeholder(Resource.Drawable.img_splash_logo).CenterCrop();
            request.Into(Target);
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
            else
            {
                return imageUrl[..index] + string.Format(UrlSuffixFormat, Target.Width, Target.Height);
            }
        }
    }
}