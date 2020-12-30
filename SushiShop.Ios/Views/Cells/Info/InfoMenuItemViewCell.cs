using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Info.Items;
using SushiShop.Ios.Common;
using System;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Info
{
    [Register(nameof(InfoMenuItemViewCell))]
    public partial class InfoMenuItemViewCell : MvxGenericView<InfoMenuItemViewModel>
    {
        private UILabel titleLabel;
        private UIImageView arrowImageView;

        protected InfoMenuItemViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            titleLabel = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = Font.Create(Core.Data.Enums.FontStyle.Regular, 16),
                Lines = 0
            };

            arrowImageView = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Image = UIImage.FromBundle(ImageNames.RightArrow)
            };

            var dividerView = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = Colors.Gray2
            };

            AddSubviews(titleLabel, arrowImageView, dividerView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                titleLabel.TopAnchor.ConstraintEqualTo(TopAnchor),
                titleLabel.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                titleLabel.LeadingAnchor.ConstraintEqualTo(LeadingAnchor, 16),
                titleLabel.TrailingAnchor.ConstraintGreaterThanOrEqualTo(arrowImageView.TrailingAnchor, 10),
                arrowImageView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor, -16),
                arrowImageView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
                dividerView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor, 16),
                dividerView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                dividerView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor, 0),
                dividerView.HeightAnchor.ConstraintEqualTo(1),
                HeightAnchor.ConstraintEqualTo(50)
            });
        }

        protected override void Bind()
        {
            var bindingSet = this.CreateBindingSet<InfoMenuItemViewCell, InfoMenuItemViewModel>();

            bindingSet.Bind(titleLabel).For(v => v.Text).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.BindTap()).To(vm => vm.ExecuteMenuActionCommand);

            bindingSet.Apply();
        }
    }
}
