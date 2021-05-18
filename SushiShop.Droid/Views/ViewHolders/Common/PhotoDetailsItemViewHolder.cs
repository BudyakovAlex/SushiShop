using Android.Graphics;
using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using Bumptech.Glide;
using UK.CO.Senab.Photoview;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Abstract;
using System;

namespace SushiShop.Droid.Views.ViewHolders.Common
{
    public class PhotoDetailsItemViewHolder : CardViewHolder<PhotoDetailsItemViewModel>
    {
        private const float MaxScale = 7f;

        private readonly Matrix matrix = new Matrix();

        private PhotoView imageView;
        private ScaleGestureDetector scaleGestureDetector;
        private PhotoViewAttacher photoViewAttacher;

        private float currentScale = 1;

        public PhotoDetailsItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        public string ImageUrl
        {
            set
            {
                if (value is null)
                {
                    return;
                }

                photoViewAttacher = new PhotoViewAttacher(imageView);
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                Glide.With(imageView.Context)
                     .Load(value)
                     .Into(imageView));

                photoViewAttacher.Update();
            }
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            imageView = view.FindViewById<PhotoView>(Resource.Id.image_view);
            imageView.SetOnTouchListener(new ViewOnTouchListener(OnImageViewTouched));
            scaleGestureDetector = new ScaleGestureDetector(view.Context, new ScaleListener(OnViewScaled));
        }

        private void OnViewScaled(ScaleGestureDetector detector)
        {
            currentScale *= detector.ScaleFactor;
            currentScale = Math.Max(0.1f, Math.Min(currentScale, MaxScale));
            matrix.SetScale(currentScale, currentScale);
            imageView.ImageMatrix = matrix;
        }

        private bool OnImageViewTouched(View view, MotionEvent motionEvent)
        {
            scaleGestureDetector.OnTouchEvent(motionEvent);

            return true;
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(nameof(ImageUrl)).To(vm => vm.ImagePath);
        }
    }
}
