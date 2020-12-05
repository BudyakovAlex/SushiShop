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
    public partial class PackageViewCell : BaseTableViewCell, IUpdatableViewCell
    {
        public static readonly NSString Key = new NSString("PackageViewCell");
        public static readonly UINib Nib;

        static PackageViewCell()
        {
            Nib = UINib.FromName("PackageViewCell", NSBundle.MainBundle);
        }

        protected PackageViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        
        public int Count
        {
            set
            {
                RefreshLayoutAction?.Invoke(this);
            }
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
            var bindingSet = this.CreateBindingSet<PackageViewCell, CartPackItemViewModel>();

            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(PriceLabel).For(v => v.Text).To(vm => vm.Price);
            bindingSet.Bind(CountStepperView).For(v => v.ViewModel).To(vm => vm.StepperViewModel);
            bindingSet.Bind(this).For(v => v.Interaction).To(vm => vm.StepperViewModel.Interaction);

            bindingSet.Apply();
        }

        private void OnInteractionRequested(object sender, EventArgs e)
        {
            RefreshLayoutAction?.Invoke(this);
        }
    }
}
