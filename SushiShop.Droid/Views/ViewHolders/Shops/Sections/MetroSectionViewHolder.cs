using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class MetroSectionViewHolder : CardViewHolder<MetroSectionViewModel>
    {
        private MvxRecyclerView recyclerView;

        public MetroSectionViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            InitializeRecyclerView(view);
        }

        public override void BindData()
        {
            base.BindData();

            using var bindingSet = CreateBindingSet();

            bindingSet.Bind(recyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
        }

        private void InitializeRecyclerView(View view)
        {
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<MetroItemViewModel, MetroItemViewHolder>(Resource.Layout.item_metro);
        }
    }
}
