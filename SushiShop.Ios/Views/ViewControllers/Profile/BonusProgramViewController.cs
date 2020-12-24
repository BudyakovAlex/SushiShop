using System;
using System.Reactive.Disposables;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreAnimation;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Converters;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxModalPresentation(
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
        ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
    public partial class BonusProgramViewController : BaseViewController<BonusProgramViewModel>
    {
        private readonly CompositeDisposable disposables;

        private nfloat startContentOffsetY;
        private bool isAtTop = true;
        private bool isFirstStart = true;

        public NSAttributedString Content
        {
            set
            {
                ContentLabel.AttributedText = value;
                ContentLabel.SizeToFit();

                ContentScrollView.ContentSize = ContentLabel.Frame.Size;
            }
        }

        public BonusProgramViewController()
        {
            disposables = new CompositeDisposable();
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            RoundedContentView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            RoundedContentView.Layer.CornerRadius = 16f;

           ContentScrollView.SubscribeToEvent(
                OnParentScrollViewDraggingStarted,
                (scrollView, handler) => scrollView.DraggingStarted += handler,
                (scrollView, handler) => scrollView.DraggingStarted -= handler)
                .DisposeWith(disposables);
            ContentScrollView.SubscribeToEvent<UIScrollView, DraggingEventArgs>(
                OnParentScrollViewDraggingEnded,
                (scrollView, handler) => scrollView.DraggingEnded += handler,
                (scrollView, handler) => scrollView.DraggingEnded -= handler)
                .DisposeWith(disposables);
            ContentScrollView.SubscribeToEvent(
                OnParentScrollViewDecelerationEnded,
                (scrollView, handler) => scrollView.DecelerationEnded += handler,
                (scrollView, handler) => scrollView.DecelerationEnded -= handler)
                .DisposeWith(disposables);
            ContentScrollView.SubscribeToEvent(
                OnParentScrollViewScrolled,
                (scrollView, handler) => scrollView.Scrolled += handler,
                (scrollView, handler) => scrollView.Scrolled -= handler)
                .DisposeWith(disposables);

            AddGestureRecognizers();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(nameof(Content)).To(vm => vm.Content)
                .WithConversion<HtmlTextToAttributedStringConverter>();
            bindingSet.Bind(TitleLabel).For(v => v.Text).To(vm => vm.Title);

            bindingSet.Apply();
        }

        private void AddGestureRecognizers()
        {
            var closeTapGesture = new UITapGestureRecognizer(() => ViewModel?.PlatformCloseCommand?.Execute(null))
            {
                ShouldReceiveTouch = CloseTapGestureShouldReceiveTouch
            };

            View.AddGestureRecognizer(closeTapGesture);
            var closeSwipeGesture = new UISwipeGestureRecognizer(() => ViewModel?.PlatformCloseCommand?.Execute(null))
            {
                Direction = UISwipeGestureRecognizerDirection.Down
            };

            View.AddGestureRecognizer(closeSwipeGesture);
        }

        private void OnParentScrollViewScrolled(object sender, EventArgs e)
        {
            if (isFirstStart)
            {
                isFirstStart = false;
                return;
            }

            if (startContentOffsetY >= ContentScrollView.ContentOffset.Y)
            {
                return;
            }

            isAtTop = false;
        }

        private void OnParentScrollViewDecelerationEnded(object sender, EventArgs e)
        {
            if (ContentScrollView.ContentOffset.Y <= 0)
            {
                isAtTop = true;
            }
        }

        private void OnParentScrollViewDraggingEnded(object sender, DraggingEventArgs e)
        {
            if (startContentOffsetY >= ContentScrollView.ContentOffset.Y && isAtTop)
            {
                ViewModel?.PlatformCloseCommand?.Execute(null);
            }
        }

        private void OnParentScrollViewDraggingStarted(object sender, System.EventArgs e)
        {
            startContentOffsetY = ContentScrollView.ContentOffset.Y;
        }

        private bool CloseTapGestureShouldReceiveTouch(UIGestureRecognizer gesture, UITouch touch)
        {
            return touch?.View == ScrollViewContentView;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                disposables?.Dispose();
            }
        }
    }
}

