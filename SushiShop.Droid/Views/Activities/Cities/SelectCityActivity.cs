using Android.App;
using AndroidX.AppCompat.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using Google.Android.Material.TextField;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Core.ViewModels.Cities.Items;
using SushiShop.Droid.Views.Activities.Abstract;
using SushiShop.Droid.Views.ViewHolders.Common;

namespace SushiShop.Droid.Views.Activities.Cities
{
    [Activity]
    public class SelectCityActivity : BaseActivity<SelectCityViewModel>
    {
        private TextInputEditText searchEditText;
        private Toolbar toolbar;
        private MvxRecyclerView recyclerView;

        public SelectCityActivity() : base(Resource.Layout.activity_select_city)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

            InitializeRecyclerView();

            searchEditText = FindViewById<TextInputEditText>(Resource.Id.search_edit_text);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            Title = AppStrings.SelectCity;
        }

        protected override void Bind()
        {
            base.Bind();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(searchEditText).For(v => v.Text).To(v => v.Query);
            bindingSet.Bind(searchEditText).For(v => v.Hint).To(v => v.QueryPlaceholder);
            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(recyclerView).For(v => v.ItemClick).To(vm => vm.SelectItemCommand);
        }

        private void InitializeRecyclerView()
        {
            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<CityItemViewModel, SelectableItemViewHolder>(Resource.Layout.item_selectable);
        }
    }
}