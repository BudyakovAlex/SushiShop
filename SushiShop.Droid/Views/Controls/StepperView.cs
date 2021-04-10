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
    [Register(nameof(SushiShop) + "." + nameof(StepperView))]
    public class StepperView : LinearLayout, IMvxBindingContextOwner, IMvxDataConsumer
    {
        private TextView valueTextView;
        private LayoutParams incrementImageLayoutParams;
        private ImageView incrementImageView;

        private LayoutParams decrementImageLayoutParams;
        private ImageView decrementImageView;

        private int imageSize;

        public StepperView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        protected StepperView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public StepperView(Context context) : base(context)
        {
            Initialize();
        }

        public StepperView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public StepperView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
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

                var margin = (int)Context.DpToPx(4);
                decrementImageLayoutParams.MarginStart = margin;
                incrementImageLayoutParams.MarginEnd = margin;

                decrementImageView.LayoutParameters = decrementImageLayoutParams;
                incrementImageView.LayoutParameters = incrementImageLayoutParams;

                SetTitle(Count.ToString());
                SetOnClickListener(null);
                return;
            }

            if (Title is null)
            {
                decrementImageView.Visibility = ViewStates.Gone;
                valueTextView.Visibility = ViewStates.Gone;
                incrementImageView.Visibility = ViewStates.Visible;

                incrementImageLayoutParams.MarginEnd = 0;
                incrementImageView.LayoutParameters = incrementImageLayoutParams;

                SetOnClickListener(null);
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

            this.SetRoundedCorners(Context.DpToPx(16));

            var layoutTransition = new LayoutTransition();
            layoutTransition.EnableTransitionType(LayoutTransitionType.Changing | LayoutTransitionType.Appearing | LayoutTransitionType.Disappearing);

            LayoutTransition = layoutTransition;
            SetBackgroundResource(Resource.Drawable.bg_button_gradient);
            Orientation = Orientation.Horizontal;

            InitializeDecrementImage();
            InitializeValueTextView();
            InitializeIncrementImage();

            this.DelayBind(Bind);
        }

        private void Bind()
        {
            using var bindingSet = this.CreateBindingSet<StepperView, StepperViewModel>();

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
                    Gravity = GravityFlags.CenterVertical
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
                Gravity = GravityFlags.CenterVertical
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
                Gravity = GravityFlags.CenterVertical
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