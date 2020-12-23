using System;
using System.Reactive.Disposables;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using CoreAnimation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Profile;
using SushiShop.Ios.Converters;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Profile
{
    [MvxModalPresentation(
        Animated = true,
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen)]
    public partial class BonusProgramViewController : BaseViewController<BonusProgramViewModel>
    {
        private nfloat startContentOffsetY;
        private bool isAtTop = true;
        private bool isFirstStart = true;
        private CompositeDisposable disposables;

        public BonusProgramViewController()
        {
            disposables = new CompositeDisposable();
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            RoundedContentView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner;
            RoundedContentView.Layer.CornerRadius = 16f;

            ParentScrollView.SubscribeToEvent(
                OnParentScrollViewDraggingStarted,
                (scrollView, handler) => scrollView.DraggingStarted += handler,
                (scrollView, handler) => scrollView.DraggingStarted -= handler)
                .DisposeWith(disposables);
            ParentScrollView.SubscribeToEvent<UIScrollView, DraggingEventArgs>(
                OnParentScrollViewDraggingEnded,
                (scrollView, handler) => scrollView.DraggingEnded += handler,
                (scrollView, handler) => scrollView.DraggingEnded -= handler)
                .DisposeWith(disposables);
            ParentScrollView.SubscribeToEvent(
                OnParentScrollViewDecelerationEnded,
                (scrollView, handler) => scrollView.DecelerationEnded += handler,
                (scrollView, handler) => scrollView.DecelerationEnded -= handler)
                .DisposeWith(disposables);
            ParentScrollView.SubscribeToEvent(
                OnParentScrollViewScrolled,
                (scrollView, handler) => scrollView.Scrolled += handler,
                (scrollView, handler) => scrollView.Scrolled -= handler)
                .DisposeWith(disposables);
            var closeTapGesture =
                new UITapGestureRecognizer(() => ViewModel?.PlatformCloseCommand?.Execute(null))
                {
                    ShouldReceiveTouch = CloseTapGestureShouldReceiveTouch
                }
                .DisposeWith(disposables);
            ScrollViewContentView.AddGestureRecognizer(closeTapGesture);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(ContentLabel).For(v => v.AttributedText).To(vm => vm.Content)
                .WithConversion<HtmlTextToAttributedStringConverter>();

            bindingSet.Apply();
        }

        private void OnParentScrollViewScrolled(object sender, EventArgs e)
        {
            if (isFirstStart)
            {
                isFirstStart = false;
                return;
            }

            if (startContentOffsetY >= ParentScrollView.ContentOffset.Y)
            {
                return;
            }

            isAtTop = false;
        }

        private void OnParentScrollViewDecelerationEnded(object sender, EventArgs e)
        {
            if (ParentScrollView.ContentOffset.Y <= 0)
            {
                isAtTop = true;
            }
        }

        private void OnParentScrollViewDraggingEnded(object sender, DraggingEventArgs e)
        {
            if (startContentOffsetY >= ParentScrollView.ContentOffset.Y && isAtTop)
            {
                ViewModel?.PlatformCloseCommand?.Execute(null);
            }
        }

        private void OnParentScrollViewDraggingStarted(object sender, System.EventArgs e)
        {
            startContentOffsetY = ParentScrollView.ContentOffset.Y;
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

