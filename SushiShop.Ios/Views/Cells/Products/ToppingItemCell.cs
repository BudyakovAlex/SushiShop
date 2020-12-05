using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.CardProduct.Items;
using SushiShop.Ios.Views.Cells.Interfaces;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Products
{
    public partial class ToppingItemCell : BaseTableViewCell, IUpdatableViewCell
    {
        public static readonly NSString Key = new NSString("ToppingItemCell");
        public static readonly UINib Nib;

        static ToppingItemCell()
        {
            Nib = UINib.FromName("ToppingItemCell", NSBundle.MainBundle);
        }

        protected ToppingItemCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        private IMvxInteraction interaction;
        public IMvxInteraction Interaction
        {
            get => interaction;
            set
            {
                if (interaction != null)
                {
                    interaction.Requested -= OnInteractionRequested;
                }

                interaction = value;
                interaction.Requested += OnInteractionRequested;
            }
        }

        public Action<UIView> RefreshLayoutAction { get; set; }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ToppingItemCell, ToppingItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(StepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(this).For(v => v.Interaction).To(vm => vm.StepperViewModel.Interaction).OneWay();

            bindingSet.Apply();
        }

        private void OnInteractionRequested(object sender, EventArgs e)
        {
            RefreshLayoutAction?.Invoke(this);
        }
    }
}
