using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace SushiShop.Ios.TargetBindings
{
    public class UIButtonImageTargetBinding : MvxTargetBinding<UIButton, string>
    {
        public UIButtonImageTargetBinding(UIButton target) : base(target)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected override void SetValue(string value)
        {
            if (value is null)
            {
                Target.SetImage(null, UIControlState.Normal);
                Target.SetImage(null, UIControlState.Highlighted);

                return;
            }

            Target.SetImage(UIImage.FromBundle(value), UIControlState.Normal);
            Target.SetImage(UIImage.FromBundle(value), UIControlState.Highlighted);
        }
    }
}

