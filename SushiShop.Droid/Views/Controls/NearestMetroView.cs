using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.DroidX.RecyclerView;
using System;
using System.Collections;

namespace SushiShop.Droid.Views.Controls
{
    [Register(nameof(SushiShop) + "." + nameof(NearestMetroView))]
    public class NearestMetroView : FrameLayout
    {
        private MvxRecyclerView recyclerView;

        public NearestMetroView(Context context) : base(context)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public NearestMetroView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected NearestMetroView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public IEnumerable MetrosCollection
        {
            get => recyclerView.ItemsSource;
            set => recyclerView.ItemsSource = value;
        }

        private void Initialize()
        {
        }
    }
}