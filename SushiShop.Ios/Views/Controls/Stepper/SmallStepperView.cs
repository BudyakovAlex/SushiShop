using System;
using Foundation;

namespace SushiShop.Ios.Views.Controls.Stepper
{
    [Register(nameof(SmallStepperView))]
    public class SmallStepperView : BaseStepperView
    {
        public SmallStepperView()
        {
        }

        protected SmallStepperView(IntPtr handle) : base(handle)
        {
        }

        protected override nfloat ButtonWidth { get; } = 32f;

        protected override nfloat TextSize { get; } = 14f;
    }
}
