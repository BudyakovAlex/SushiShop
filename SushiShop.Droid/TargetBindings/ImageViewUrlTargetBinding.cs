using Android.Widget;
using BumpTech.GlideLib;
using MvvmCross.Platforms.Android.Binding.Target;

namespace SushiShop.Droid.TargetBindings
{
    public class ImageViewUrlTargetBinding : MvxAndroidTargetBinding<ImageView, string>
    {
        public ImageViewUrlTargetBinding(ImageView target) : base(target)
        {
        }

        protected override void SetValueImpl(ImageView target, string value)
        {
            Glide.With(target.Context)
                .Load(value)
                .Into(target);
        }
    }
}