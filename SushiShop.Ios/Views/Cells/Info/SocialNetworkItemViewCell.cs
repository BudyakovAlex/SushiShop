using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using FFImageLoading.Cross;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Info.Items;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Info
{
    [Register(nameof(SocialNetworkItemViewCell))]
    public partial class SocialNetworkItemViewCell : MvxGenericView<SocialNetworkItemViewModel>
    {
        private MvxCachedImageView imageView;

        protected SocialNetworkItemViewCell(IntPtr handle) : base(handle)
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
                imageView.HeightAnchor.ConstraintEqualTo(50),
                imageView.WidthAnchor.ConstraintEqualTo(50),
            });
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<SocialNetworkItemViewCell, SocialNetworkItemViewModel>();

            bindingSet.Bind(imageView).For(v => v.ImagePath).To(vm => vm.ImageUrl);
            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.OpenBrowserCommand);

            bindingSet.Apply();
        }
    }
}