using Android.App;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.TextField;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Cities;
using SushiShop.Droid.Views.Activities.Abstract;

namespace SushiShop.Droid.Views.Activities.Cities
{
    [Activity]
    public class SelectCityActivity : BaseActivity<SelectCityViewModel>
    {
        private TextInputEditText searchEditText;
        private Toolbar toolbar;

        public SelectCityActivity() : base(Resource.Layout.activity_select_city)
        {
        }

        protected override void InitializeViewPoroperties()
        {
            base.InitializeViewPoroperties();

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
        }
    }
}
