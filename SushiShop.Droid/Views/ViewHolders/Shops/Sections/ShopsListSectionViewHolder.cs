using Android.Views;
using AndroidX.RecyclerView.Widget;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapter.TemplateSelectors;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Adapters;
using Google.Android.Material.TextField;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using SushiShop.Core.IoC;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Core.ViewModels.Shops.Sections;
using SushiShop.Droid.Extensions;
using SushiShop.Droid.Views.Controllers;
using SushiShop.Droid.Views.Controls;
using SushiShop.Droid.Views.Listeners;
using SushiShop.Droid.Views.ViewHolders.Abstract;
using System;
using Xamarin.Essentials;

namespace SushiShop.Droid.Views.ViewHolders.Shops.Sections
{
    public class ShopsListSectionViewHolder : CardViewHolder<ShopsListSectionViewModel>, RecyclerView.IOnItemTouchListener
    {
        private readonly ITabLayoutController tabLayoutController;

        private View view;
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

        private MvxInteraction removeFocusInteraction;
        public MvxInteraction RemoveFocusInteraction
        {
            get => removeFocusInteraction;
            set
            {
                if (removeFocusInteraction != null)
                {
                    removeFocusInteraction.Requested -= OnRemoveFocusInteractionRequested;
                }

                removeFocusInteraction = value;
                if (removeFocusInteraction != null)
                {
                    removeFocusInteraction.Requested += OnRemoveFocusInteractionRequested;
                }
            }
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
            bindingSet.Bind(this).For(v => v.RemoveFocusInteraction).To(vm => vm.RemoveFocusInteraction);
        }

        protected override void DoInit(View view)
        {
            base.DoInit(view);

            this.view = view;
            searchEditText = view.FindViewById<TextInputEditText>(Resource.Id.search_edit_text);
            shopsListRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.shops_list_recycler_view);
            nearestMetroView = view.FindViewById<NearestMetroView>(Resource.Id.nearest_metro_view);
            searchEditText.Hint = AppStrings.EnterAddress;

            searchEditText.SetOnKeyListener(new ViewOnKeyListener(OnKeyRequested));
            shopsListRecyclerView.AddOnItemTouchListener(this);

            InitializeShopsListRecyckerView();
        }

        private void OnRemoveFocusInteractionRequested(object sender, EventArgs e) =>
            view.Context.HideKeyboard(searchEditText.WindowToken);

        private async System.Threading.Tasks.Task HideBottomTabsAsync()
        {
            tabLayoutController.Hide();
            await System.Threading.Tasks.Task.Delay(200);
            await MainThread.InvokeOnMainThreadAsync(nearestMetroView.Show);
        }

        private bool OnKeyRequested(View view, Keycode keyCode, KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                switch (keyCode)
                {
                    case Keycode.DpadCenter:
                    case Keycode.Enter:
                        view.Context.HideKeyboard(searchEditText.WindowToken);
                        return true;
                    default:
                        break;
                }
            }

            return false;
        }

        private void InitializeShopsListRecyckerView()
        {
            shopsListRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);
            shopsListRecyclerView.ItemTemplateSelector = new TemplateSelector()
                .AddElement<ShopItemViewModel, ShopItemViewHolder>(Resource.Layout.item_shop);
        }

        public bool OnInterceptTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {
            view.Context.HideKeyboard(searchEditText.WindowToken);
            return false;
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallow)
        {
        }

        public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {
        }
    }
}
