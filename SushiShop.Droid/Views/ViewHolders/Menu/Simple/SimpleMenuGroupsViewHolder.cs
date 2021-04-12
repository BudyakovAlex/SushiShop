using Android.Views;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Droid.Views.ViewHolders.Abstract;

namespace SushiShop.Droid.Views.ViewHolders.Menu.Simple
{
    public class SimpleMenuGroupsViewHolder : CardViewHolder<GroupsMenuItemViewModel>
    {
        private MvxRecyclerView recyclerView;

        public SimpleMenuGroupsViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
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

        private void InitializeRecyclerView(View parentView)
        {
            recyclerView = parentView as MvxRecyclerView;
            recyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            recyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<GroupMenuItemViewModel, SimpleGroupMenuItemViewHolder>(Resource.Layout.item_group_menu);
        }
    }
}
