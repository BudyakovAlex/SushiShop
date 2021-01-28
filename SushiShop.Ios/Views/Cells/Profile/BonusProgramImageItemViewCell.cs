using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using FFImageLoading.Cross;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.ViewModels.Profile.Items;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Profile
{
    [Register(nameof(BonusProgramImageItemViewCell))]
    public partial class BonusProgramImageItemViewCell : MvxGenericView<BonusProgramImageItemViewModel>
    {
        private MvxCachedImageView imageView;

        protected BonusProgramImageItemViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            imageView = new MvxCachedImageView { TranslatesAutoresizingMaskIntoConstraints = false };
            AddSubviews(imageView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                imageView.TopAnchor.ConstraintEqualTo(TopAnchor),
                imageView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                imageView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                imageView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                imageView.HeightAnchor.ConstraintEqualTo(70),
                imageView.WidthAnchor.ConstraintEqualTo(70),
            });
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<BonusProgramImageItemViewCell, BonusProgramImageItemViewModel>();

            bindingSet.Bind(imageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);

            bindingSet.Apply();
        }
    }
}
