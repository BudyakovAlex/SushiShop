using Android.Gms.Tasks;
using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using Google.Android.Material.TextField;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.IoC;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Views.Controllers;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.ViewHolders.Abstract;
using Xamarin.Essentials;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class ShopsListSectionViewHolder : CardViewHolder<ShopsListSectionViewModel>
    {
        private readonly ITabLayoutController tabLayoutController;

        private TextInputEditText searchEditText;
        private MvxRecyclerView shopsListRecyclerView;
        private NearestMetroView nearestMetroView;

        public ShopsListSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
            tabLayoutController = CompositionRoot.Container.Resolve<ITabLayoutController>();
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
                    _ = HideBottomTabsAsync();
                    return;
                }

                nearestMetroView.Hide(tabLayoutController.Show);
            }
        }

        private async System.Threading.Tasks.Task HideBottomTabsAsync()
        {
            tabLayoutController.Hide();
            await System.Threading.Tasks.Task.Delay(200);
            await MainThread.InvokeOnMainThreadAsync(nearestMetroView.Show);
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            searchEditText = view.FindViewById<TextInputEditText>(Resource.Id.search_edit_text);
            shopsListRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.shops_list_recycler_view);
            nearestMetroView = view.FindViewById<NearestMetroView>(Resource.Id.nearest_metro_view);

            InitializeShopsListRecyckerView();
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(shopsListRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(searchEditText).For(v => v.Text).To(vm => vm.Query).TwoWay();
            bindingSet.Bind(nearestMetroView).For(v => v.MetrosCollection).To(vm => vm.NearestMetro);
            bindingSet.Bind(nearestMetroView).For(v => v.CloseCommand).To(vm => vm.ClearNearestMetroCommand);
            bindingSet.Bind(this).For(v => v.IsNearestMetroNotEmpty).To(vm => vm.IsNearestMetroNotEmpty);
        }

        private void InitializeShopsListRecyckerView()
        {
            shopsListRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            shopsListRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ShopItemViewModel, ShopItemViewHolder>(Resource.Layout.item_shop);
        }
    }
}
