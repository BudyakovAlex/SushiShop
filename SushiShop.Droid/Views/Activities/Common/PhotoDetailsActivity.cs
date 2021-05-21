using Android.App;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.ViewHolders.Common;

namespace SushiShop.Droid.Views.Activities.Common
{
    [Activity]
    public class PhotoDetailsActivity : BaseActivity<PhotoDetailsViewModel>
    {
        private MvxRecyclerView recyclerView;
        private Toolbar toolbar;

        public PhotoDetailsActivity() : base(Resource.Layout.activity_photo_details)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            InitializeRecyclerView();

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
        }

        private void InitializeRecyclerView()
        {
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.SetLayoutManager(new MvxGuardedLinearLayoutManager(this) { Orientation = LinearLayoutManager.Horizontal });
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<PhotoDetailsItemViewModel, PhotoDetailsItemViewHolder>(Resource.Layout.item_photo_details);

            var snapHelper = new PagerSnapHelper();
            snapHelper.AttachToRecyclerView(recyclerView);
        }
    }
}