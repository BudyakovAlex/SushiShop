using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Listeners;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Core.ViewModels.Profile.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.ViewHolders.Profile;

namespace SushiShop.Droid.Views.Fragments.Profile
{
    [MvxFragmentPresentation(FragmentContentId = Resource.Id.container_view)]
    public class BonusProgramFragment : BaseFragment<BonusProgramViewModel>
    {
        private ConstraintLayout backgroundView;
        private TextView titleTextView;
        private LinearLayout contentLinearLayout;
        private WebView webView;
        private MvxRecyclerView imagesRecyclerView;
        private bool isMoved;
        private float startY;
        private bool webViewAtTop = true;

        public BonusProgramFragment() : base(Resource.Layout.fragment_bonus_program)
        {
        }

        public string Content
        {
            set => webView.LoadDataWithBaseURL(null, value, "text/html", "utf-8", null);
        }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            backgroundView = view.FindViewById<ConstraintLayout>(Resource.Id.background_view);
            webView = view.FindViewById<WebView>(Resource.Id.content_web_view);
            titleTextView = view.FindViewById<TextView>(Resource.Id.title_text_view);
            contentLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.content_linear_layout);
            InitializeImagesRecyclerView();

            contentLinearLayout.SetOnTouchListener(new ViewOnTouchListener((v, e) =>
            {
                ActionTouch(e);
                return true;
            }));
            webView.Touch += WebViewTouch;
            webView.ScrollChange += WebViewScrollChange;

            contentLinearLayout.SetTopRoundedCorners(view.Context.DpToPx(25));
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(nameof(Content)).To(vm => vm.Content);
            bindingSet.Bind(titleTextView).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(backgroundView).For(v => v.BindClick()).To(vm => vm.CloseCommand);
            bindingSet.Bind(imagesRecyclerView).For(v => v.ItemsSource).To(vm => vm.Images);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (webView != null)
            {
                webView.Touch -= WebViewTouch;
                webView.ScrollChange -= WebViewScrollChange;
            }
        }

        private void InitializeImagesRecyclerView()
        {
            imagesRecyclerView = View.FindViewById<MvxRecyclerView>(Resource.Id.images_recycler_view);

            imagesRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            imagesRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<BonusProgramImageItemViewModel, ImageBonusProgramItemViewHolder>(Resource.Layout.item_image_bouns_program);
            var layoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            imagesRecyclerView.SetLayoutManager(layoutManager);
            imagesRecyclerView.AddItemDecoration(new SpacesItemDecoration(CalculateItemSpace));
        }

        private Rect CalculateItemSpace(int position, Rect rect)
        {
            if (position == 0)
            {
                rect.Left = (int)View.Context.DpToPx(35);
            }
            else
            {
                var width = (int)View.Context.DpToPx(35);
                rect.Left = (int)(View.Width - width * 2 - View.Context.DpToPx(70) * 3) / 2;
            }

            return rect;
        }

        private void WebViewScrollChange(object sender, View.ScrollChangeEventArgs e)
        {
            webViewAtTop = e.ScrollY == 0;
        }

        private void WebViewTouch(object sender, View.TouchEventArgs e)
        {
            webView.OnTouchEvent(e.Event);
            if (webViewAtTop)
            {
                ActionTouch(e.Event);
            }
        }

        private void ActionTouch(MotionEvent e)
        {
            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    startY = e.GetY();
                    isMoved = false;
                    break;
                case MotionEventActions.Move:
                    isMoved = true;
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    if (isMoved && e.GetY() > startY)
                    {
                        ViewModel?.CloseCommand?.Execute(null);
                    }

                    break;
            }
        }
    }
}
