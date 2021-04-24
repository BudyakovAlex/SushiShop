using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Core.ViewModels.Common.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Common;
using System;
using System.Threading.Tasks;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Common
{
    [MvxChildPresentation]
    public partial class PhotoDetailsViewController : BaseViewController<PhotoDetailsViewModel>
    {
        private MainViewController rootViewController = (MainViewController)UIApplication.SharedApplication.KeyWindow.RootViewController;
        private CollectionViewSource _source;

        public int CurrentIndex { set => _ = ScrollWithDelayAsync(value); }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            rootViewController.HideTabView();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            rootViewController.ShowTabView();
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            InitializeCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(nameof(CurrentIndex)).To(vm => vm.CurrentIndex);
            bindingSet.Bind(_source).For(v => v.ItemsSource).To(vm => vm.Items);

            bindingSet.Apply();
        }

        private void InitializeCollectionView()
        {
            _source = new CollectionViewSource(PhotosCollectionView)
                .Register<PhotoDetailsItemViewModel>(PhotoDetailsItemViewCell.Nib, PhotoDetailsItemViewCell.Key);

            PhotosCollectionView.Source = _source;
            PhotosCollectionView.Delegate = new PhotoDetailsCollectionViewDelegateFlowLayout();
        }

        private async Task ScrollWithDelayAsync(int value)
        {
            try
            {
                await Task.Delay(200);
                await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(
                    () =>
                    {
                        var frame = PhotosCollectionView.GetLayoutAttributesForItem(NSIndexPath.FromItemSection(value, 0))?.Frame;
                        if (frame is null)
                        {
                            return;
                        }

                        PhotosCollectionView.ScrollRectToVisible(frame.Value, false);
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
