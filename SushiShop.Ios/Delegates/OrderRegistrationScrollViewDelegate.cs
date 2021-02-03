using System;
using UIKit;

namespace SushiShop.Ios.Delegates
{
    public class OrderRegistrationScrollViewDelegate : UIScrollViewDelegate
    {
        private readonly Action onDecelereted;

        public OrderRegistrationScrollViewDelegate(Action onDecelereted)
        {
            this.onDecelereted = onDecelereted;
        }

        public override void DecelerationEnded(UIScrollView scrollView)
        {
            onDecelereted?.Invoke();
        }
    }
}
