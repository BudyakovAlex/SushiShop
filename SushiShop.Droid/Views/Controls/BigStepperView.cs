using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content.Resources;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding;
using SushiShop.Core.ViewModels.Common;
using System;
using System.Threading.Tasks;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(BigStepperView))]
    public class BigStepperView : FrameLayout, IMvxBindingContextOwner, IMvxDataConsumer
    {
        private TextView valueTextView;
        private LayoutParams incrementImageLayoutParams;
        private ImageView incrementImageView;

        private LayoutParams decrementImageLayoutParams;
        private ImageView decrementImageView;

        private int imageSize;

        public BigStepperView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        protected BigStepperView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public BigStepperView(Context context) : base(context)
        {
            Initialize();
        }

        public BigStepperView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public BigStepperView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                SetControlsState();
                RequestLayout();
            }
        }

        public IMvxBindingContext BindingContext { get; set; }

        public StepperViewModel ViewModel => BindingContext?.DataContext as StepperViewModel;

        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                SetControlsState();
                RequestLayout();
            }
        }

        public object DataContext
        {
            get => BindingContext.DataContext;
            set => BindingContext.DataContext = value;
        }

        private void SetTitle(string title)
        {
            valueTextView.Text = title;
            valueTextView.Visibility = ViewStates.Visible;
        }

        private void SetControlsState()
        {
            if (Count > 0)
            {
                decrementImageView.Visibility = ViewStates.Visible;
                incrementImageView.Visibility = ViewStates.Visible;

                var margin = (int)Context.DpToPx(16);
                decrementImageLayoutParams.MarginStart = margin;
                incrementImageLayoutParams.MarginEnd = margin;

                decrementImageView.LayoutParameters = decrementImageLayoutParams;
                incrementImageView.LayoutParameters = incrementImageLayoutParams;

                SetTitle(Count.ToString());
                SetOnClickListener(null);
                RequestLayout();
                return;
            }

            decrementImageView.Visibility = ViewStates.Gone;
            incrementImageView.Visibility = ViewStates.Gone;

            SetTitle(Title);
            SetOnClickListener(new ViewOnClickListener((v) =>
            {
                ViewModel?.AddCommand?.Execute();
                return Task.CompletedTask;
            }));
        }

        private void Initialize()
        {
            imageSize = (int)Context.DpToPx(32);

            BindingContext = new MvxBindingContext();

            this.SetRoundedCorners(Context.DpToPx(25));

            var layoutTransition = new LayoutTransition();
            layoutTransition.EnableTransitionType(LayoutTransitionType.Changing | LayoutTransitionType.Appearing | LayoutTransitionType.Disappearing);

            LayoutTransition = layoutTransition;
            SetBackgroundResource(Resource.Drawable.bg_button_gradient);

            InitializeDecrementImage();
            InitializeValueTextView();
            InitializeIncrementImage();

            this.DelayBind(Bind);
        }

        private void Bind()
        {
            using var bindingSet = this.CreateBindingSet<BigStepperView, StepperViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.Count).To(vm => vm.Count);

            bindingSet.Bind(decrementImageView).For(v => v.BindClick()).To(vm => vm.RemoveCommand);
            bindingSet.Bind(incrementImageView).For(v => v.BindClick()).To(vm => vm.AddCommand);
        }

        private void InitializeValueTextView()
        {
            valueTextView = new TextView(Context)
            {
                LayoutParameters = new LayoutParams(
                   ViewGroup.LayoutParams.WrapContent,
                   ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = GravityFlags.Center,
                    MarginStart = imageSize,
                    MarginEnd = imageSize
                },
                TextSize = 14
            };

            valueTextView.SetTextColor(Color.White);
            var typeface = ResourcesCompat.GetFont(Context, Resource.Font.sf_pro_display_medium);
            valueTextView.SetTypeface(typeface, TypefaceStyle.Normal);
            AddView(valueTextView);
        }

        private void InitializeIncrementImage()
        {
            incrementImageLayoutParams = new LayoutParams(imageSize, imageSize)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.End
            };

            incrementImageView = new ImageView(Context)
            {
                LayoutParameters = incrementImageLayoutParams
            };

            incrementImageView.SetImageResource(Resource.Drawable.ic_plus);
            incrementImageView.SetScaleType(ImageView.ScaleType.Center);
            AddView(incrementImageView);
        }

        private void InitializeDecrementImage()
        {
            decrementImageLayoutParams = new LayoutParams(imageSize, imageSize)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.Start
            };

            decrementImageView = new ImageView(Context)
            {
                LayoutParameters = decrementImageLayoutParams,
            };

            decrementImageView.SetImageResource(Resource.Drawable.ic_minus);
            decrementImageView.SetScaleType(ImageView.ScaleType.Center);
            AddView(decrementImageView);
        }
    }
}