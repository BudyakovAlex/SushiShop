using System;
using System.Drawing;
using AndroidX.CardView.Widget;
using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;
using Xamarin.Essentials;

namespace SushiShop.Droid.TargetBindings
{
    public class CardViewBackgroundColorTargetBinding : MvxAndroidTargetBinding<CardView, Color>
    {
        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;
        
        public CardViewBackgroundColorTargetBinding(CardView target) : base(target)
        {
        }

        protected override void SetValueImpl(CardView target, Color value)
        {
            target.SetCardBackgroundColor(value.ToPlatformColor());
        }
    }
}