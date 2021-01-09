using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;
using SushiShop.Core.ViewModels.Cart.Items;
using SushiShop.Ios.Views.Cells.Interfaces;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Cart
{
    public partial class CartToppingViewCell : BaseTableViewCell, IUpdatableViewCell
    {
        public static readonly NSString Key = new NSString("CartToppingViewCell");
        public static readonly UINib Nib;

        static CartToppingViewCell()
        {
            Nib = UINib.FromName("CartToppingViewCell", NSBundle.MainBundle);
        }

        protected CartToppingViewCell(IntPtr handle) : base(handle)
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
            var bindignSet = this.CreateBindingSet<CartToppingViewCell, CartToppingItemViewModel>();

            bindignSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindignSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindignSet.Bind(CountStepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindignSet.Bind(this).For(v => v.Interaction).To(vm => vm.StepperViewModel.Interaction);

            bindignSet.Apply();
        }

        private void OnInteractionRequested(object sender, EventArgs e)
        {
            RefreshLayoutAction?.Invoke(this);
        }
    }
}
