using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Fragments;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels;
using SushiShop.Core.ViewModels.Info;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Droid.Views.Decorators;
using SushiShop.Droid.Views.Fragments.Abstract;
using SushiShop.Droid.Views.ViewHolders.Info;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace SushiShop.Droid.Views.Fragments.Info
{
    [MvxTabLayoutPresentation(
        TabLayoutResourceId = Resource.Id.main_tab_layout,
        ViewPagerResourceId = Resource.Id.main_view_pager,
        ActivityHostViewModelType = typeof(MainViewModel))]
    public class InfoFragment : BaseFragment<InfoViewModel>, ITabFragment
    {
        private ConstraintLayout officeLinearLayout;
        private TextView officePhoneTextView;
        private LinearLayout shopsLinearLayout;
        private MvxRecyclerView menuRecyclerView;
        private MvxRecyclerView socialNetworksRecyclerView;

        public InfoFragment()
            : base(Resource.Layout.fragment_info)
        {
        }

        public bool IsActivated { get; set; }

        protected override void InitializeViewPoroperties(View view, Bundle savedInstanceState)
        {
            base.InitializeViewPoroperties(view, savedInstanceState);

            officeLinearLayout = view.FindViewById<ConstraintLayout>(Resource.Id.office_constraint_layout);
            officePhoneTextView = officeLinearLayout.FindViewById<TextView>(Resource.Id.office_phone_text_view);
            shopsLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.shops_linear_layout);
            menuRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.menu_recycler_view);
            socialNetworksRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.social_networks_recycler_view);

            InitializeMenuRecylerView();
            InitializeSocialNetworksRecyclerView();

            view.FindViewById<Toolbar>(Resource.Id.toolbar).Title = AppStrings.Info;
            officeLinearLayout.FindViewById<TextView>(Resource.Id.office_title_text_view).Text = AppStrings.Office;
            shopsLinearLayout.FindViewById<TextView>(Resource.Id.shops_title_text_view).Text = AppStrings.Shops;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(officeLinearLayout).For(v => v.BindClick()).To(vm => vm.CallToOfficeCommand);
            bindingSet.Bind(officeLinearLayout).For(v => v.BindVisible()).To(vm => vm.HasOfficePhone);
            bindingSet.Bind(officePhoneTextView).For(v => v.Text).To(vm => vm.OfficePhone);
            bindingSet.Bind(shopsLinearLayout).For(v => v.BindClick()).To(vm => vm.GoToShopsCommand);
            bindingSet.Bind(menuRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(socialNetworksRecyclerView).For(v => v.ItemsSource).To(vm => vm.SocialNetworks);
        }

        private void InitializeMenuRecylerView()
        {
            menuRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            menuRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<InfoMenuItemViewModel, InfoMenuItemViewHolder>(Resource.Layout.item_info_menu);
        }

        private void InitializeSocialNetworksRecyclerView()
        {
            socialNetworksRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            socialNetworksRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<SocialNetworkItemViewModel, SocialNetworkItemViewHolder>(Resource.Layout.item_info_social_network);
            var layoutManager = new MvxGuardedLinearLayoutManager(Context) { Orientation = MvxGuardedLinearLayoutManager.Horizontal };
            socialNetworksRecyclerView.SetLayoutManager(layoutManager);
            socialNetworksRecyclerView.AddItemDecoration(new SpacesItemDecoration(CalculateItemSpace));
        }

        private Rect CalculateItemSpace(int position, Rect rect)
        {
            rect.Left = position == 0 ? 0 : (int)Context.DpToPx(20);
            return rect;
        }
    }
}