using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace SushiShop.Ios.TargetBindings
{
    public class UIButtonImageTargetBinding : MvxTargetBinding<UIButton, string>
    {
        public UIButtonImageTargetBinding(UIButton target)
            : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(string value)
        {
            var image = value is null
                ? null
                : UIImage.FromBundle(value).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            Target.SetImage(image, UIControlState.Normal);
        }
    }
}

