using Android.App;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.ViewHolders.Toppings;

namespace SushiShop.Droid.Views.Activities.Shops
{
    [Activity]
    public class ShopsNearMetroActivity : BaseActivity<ShopsNearMetroViewModel>
    {
        private Toolbar toolbar;
        private MvxRecyclerView recyclerView;

        public ShopsNearMetroActivity() : base(Resource.Layout.activity_shops_near_metro)
        {
        }

        private bool isNearestMetroNotEmpty;
        public bool IsNearestMetroNotEmpty
        {
            get => isNearestMetroNotEmpty;
            set
            {
                isNearestMetroNotEmpty = value;
                if (value)
                {
                    //nearestMetroView.Show();
                    return;
                }

                //nearestMetroView.Hide();
            }
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

            bindingSet.Bind(toolbar).For(v => v.BindBackNavigationItemCommand()).To(vm => vm.CloseCommand);
            bindingSet.Bind(toolbar).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);

            //bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
            //bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);
        }

        private void InitializeRecyclerView()
        {
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            //recyclerView.ItemTemplateSelector = new TemplateSelector()
            //    .AddElement<ShopItemViewModel, ShopItemViewHolder>(Resource.Layout.item_shop);
        }
    }
}
