using System;
using System.Collections;
using System.Windows.Input;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Controls;
using CoreAnimation;
using CoreFoundation;
using Foundation;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Shops;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(NearestMetroView))]
    public partial class NearestMetroView : MvxGenericView<NearestMetroView>
    {
        private TableViewSource tableViewSource;

        public NearestMetroView(IntPtr handle) : base(handle)
        {
        }

        public IEnumerable MetrosCollection
        {
            get => tableViewSource.ItemsSource;
            set => tableViewSource.ItemsSource = value;
        }

        public ICommand CloseCommand { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            Alpha = 0f;

            TranslatesAutoresizingMaskIntoConstraints = false;

            RootContentView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            RootContentView.Layer.CornerRadius = 16f;
            tableViewSource = new TableViewSource(MetroTableView);
            tableViewSource.Register<MetroItemViewModel>(NearestMetroItemViewCell.Nib, NearestMetroItemViewCell.Key);
            MetroTableView.Source = tableViewSource;

            AddGestureRecognizer(new UITapGestureRecognizer(() => CloseCommand?.Execute(null))
            {
                ShouldReceiveTouch = CloseTapGestureShouldReceiveTouch
            });
        }

        public void Show()
        {
            UIApplication.SharedApplication.KeyWindow.AddSubview(this);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                TopAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.TopAnchor),
                LeadingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.LeadingAnchor),
                TrailingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.TrailingAnchor),
                BottomAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.BottomAnchor)
            });
            AnimateAlpha(1f);
        }

        public void Hide()
        {
            AnimateAlpha(0f);
            RemoveFromSuperview();
        }

        private void AnimateAlpha(float alpha)
        {
            DispatchQueue.MainQueue.DispatchAsync(async () =>
            await AnimateAsync(
                0.2f,
                () => Alpha = alpha));
        }

        private bool CloseTapGestureShouldReceiveTouch(UIGestureRecognizer gesture, UITouch touch)
        {
            return touch?.View == this;
        }
    }
}
