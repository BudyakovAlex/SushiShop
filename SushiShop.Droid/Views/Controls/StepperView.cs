using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.Views;
using SushiShop.Core.Converters;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(StepperView))]
    public class StepperView : MvxLinearLayout, IMvxBindingContextOwner
    {
        private TextView valueTextView;
        private ImageView incrementImageView;
        private ImageView decrementImageView;

        public StepperView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public StepperView(Context context, IAttributeSet attrs, IMvxAdapterWithChangedEvent adapter) : base(context, attrs, adapter)
        {
            Initialize();
        }

        protected StepperView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public string Title { get; set; }

        public IMvxBindingContext BindingContext { get; set; }

        public StepperViewModel ViewModel => BindingContext?.DataContext as StepperViewModel;

        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                if (count == 0)
                {
                    if (Title is null)
                    {
                        decrementImageView.Visibility = ViewStates.Gone;
                        valueTextView.Visibility = ViewStates.Gone;
                        incrementImageView.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        decrementImageView.Visibility = ViewStates.Gone;
                        incrementImageView.Visibility = ViewStates.Gone;

                        SetTitle(Title);
                        SetOnClickListener(new ViewOnClickListener((v) =>
                        {
                            ViewModel?.AddCommand?.Execute();
                            return Task.CompletedTask;
                        }));
                    }
                }
                else
                {
                    decrementImageView.Visibility = ViewStates.Visible;
                    incrementImageView.Visibility = ViewStates.Visible;
                    SetTitle(count.ToString());
                    SetOnClickListener(null);
                }

                RequestLayout();
            }
        }

        private void SetTitle(string title)
        {
            valueTextView.Text = title;
            valueTextView.Visibility = ViewStates.Visible;
        }

        private void Initialize()
        {
            LayoutTransition.EnableTransitionType(LayoutTransitionType.Changing);
            SetBackgroundResource(Resource.Drawable.bg_button_gradient);
            Orientation = Android.Widget.Orientation.Horizontal;

            InitializeDecrementImage();
            InitializeValueTextView();
            InitializeIncrementImage();

            BindingContext = new MvxBindingContext();
            this.DelayBind(Bind);
        }

        private void Bind()
        {
            using var bindingSet = this.CreateBindingSet<StepperView, StepperViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.Count).To(vm => vm.Count);
            bindingSet.Bind(decrementImageView).For(v => v.BindClick()).To(vm => vm.RemoveCommand);
            bindingSet.Bind(incrementImageView).For(v => v.BindClick()).To(vm => vm.AddCommand);
            bindingSet.Bind(incrementImageView).For(v => v.BindMarginEnd()).To(vm => vm.Count)
               .WithConversion(new DelegateConverter<int, int>((int count) => count == 0 ? 10 : 12));
            bindingSet.Bind(incrementImageView).For(v => v.BindMarginStart()).To(vm => vm.Count)
               .WithConversion(new DelegateConverter<int, int>((int count) => count == 0 ? 10 : 0));
        }

        private void InitializeValueTextView()
        {
            var margin = (int)Context.DpToPx(6);
            valueTextView = new TextView(Context)
            {
                LayoutParameters = new LayoutParams(
                   ViewGroup.LayoutParams.WrapContent,
                   ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = GravityFlags.CenterVertical,
                    MarginStart = margin,
                    MarginEnd = margin
                },
                TextSize = Context.DpToPx(14)
            };

            valueTextView.SetTextColor(Color.White);
            var typeface = Typeface.CreateFromAsset(Context.Assets,
            "font/sf_pro_display_regular.otf");
            valueTextView.SetTypeface(typeface, TypefaceStyle.Normal);
            AddView(valueTextView);
        }

        private void InitializeIncrementImage()
        {
            var layoutParams = new LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical
            };

            incrementImageView = new ImageView(Context)
            {
                LayoutParameters = layoutParams
            };

            incrementImageView.SetImageResource(Resource.Drawable.ic_plus);
            AddView(valueTextView);
        }

        private void InitializeDecrementImage()
        {
            var layoutParams = new LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical,
                MarginStart = (int)Context.DpToPx(12)
            };

            decrementImageView = new ImageView(Context)
            {
                LayoutParameters = layoutParams
            };

            decrementImageView.SetImageResource(Resource.Drawable.ic_minus);
            AddView(valueTextView);
        }
    }
}