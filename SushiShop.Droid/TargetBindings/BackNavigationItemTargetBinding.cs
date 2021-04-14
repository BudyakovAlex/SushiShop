using System.Windows.Input;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding.Target;

namespace SushiShop.Droid.TargetBindings
{
    public class BackNavigaitonItemTargetBinding : MvxAndroidTargetBinding<Toolbar, ICommand>
    {
        public const string DefaultBackNavigaitonItemTargetBinding = nameof(DefaultBackNavigaitonItemTargetBinding);

        private ICommand command;

        public BackNavigaitonItemTargetBinding(Toolbar target) : base(target)
        {
        }

        protected override void SetValueImpl(Toolbar target, ICommand value)
        {
            command = value;
            Target.NavigationClick += ToolbarNavigationClick;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing && Target != null)
            {
                Target.NavigationClick -= ToolbarNavigationClick;
            }
        }

        private void ToolbarNavigationClick(object sender, Toolbar.NavigationClickEventArgs e)
        {
            command?.Execute(null);
        }
    }
}
