using System;
using Foundation;

namespace SushiShop.Ios.Views.Controls.Stepper
{
    [Register(nameof(BigStepperView))]
    public class BigStepperView : BaseStepperView
    {
        public BigStepperView()
        {
        }

        protected BigStepperView(IntPtr handle)
            : base(handle)
        {
        }

        protected override nfloat ButtonWidth { get; } = 62f;

        protected override nfloat TextSize { get; } = 18f;
    }
}
