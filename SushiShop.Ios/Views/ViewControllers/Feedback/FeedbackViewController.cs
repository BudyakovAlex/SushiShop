using System;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.ViewControllers;
using Foundation;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using SushiShop.Core.ViewModels.Feedback;
using SushiShop.Core.ViewModels.Feedback.Items;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Sources;
using SushiShop.Ios.Views.Cells.Feedback;
using UIKit;

namespace SushiShop.Ios.Views.ViewControllers.Feedback
{
    [MvxChildPresentation]
    public partial class FeedbackViewController : BaseViewController<FeedbackViewModel>
    {
        private CollectionViewSource _source;

        private NSObject keyboardWillShowObject;
        private NSObject keyboardWillHideObject;

        private bool hasPhotos;
        public bool HasPhotos
        {
            get => hasPhotos;
            set
            {
                if (hasPhotos == value)
                {
                    return;
                }

                hasPhotos = value;
                UIView.Animate(0.5f, () =>
                {
                    PhotosCollectionViewHeightConstraint.Constant = hasPhotos ? 100f : 0f;
                    ScrollView.LayoutIfNeeded();
                });
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            AddKeyboardObservers();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            RemoveKeyboardObservers();
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            View.AddGestureRecognizer(new UITapGestureRecognizer(OnViewTap)
            {
                CancelsTouchesInView = false
            });

            InitializeScrollView();
            InitializeOrderNumberTextField();
            InitializeCollectionView();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = CreateBindingSet();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.HasPhotos).To(vm => vm.HasPhotos);
            bindingSet.Bind(OrderNumberTextField).For(v => v.Placeholder).To(vm => vm.OrderNumberPlaceholder);
            bindingSet.Bind(OrderNumberTextField).For(v => v.Text).To(vm => vm.OrderNumber);
            bindingSet.Bind(QuestionTextView).For(v => v.Placeholder).To(vm => vm.QuestionPlaceholder);
            bindingSet.Bind(QuestionTextView).For(v => v.Text).To(vm => vm.Question);
            bindingSet.Bind(UploadPhotosView).For(v => v.BindTap()).To(vm => vm.UploadPhotosCommand);
            bindingSet.Bind(UploadPhotosLabel).For(v => v.Text).To(vm => vm.UploadPhotosTitle);
            bindingSet.Bind(_source).For(v => v.ItemsSource).To(vm => vm.Photos);
            bindingSet.Bind(SendButton).For(v => v.BindTitle()).To(vm => vm.SendFeedbackTitle);
            bindingSet.Bind(SendButton).For(v => v.BindTouchUpInside()).To(vm => vm.SendFeedbackCommand);
            bindingSet.Bind(LoadingView).For(v => v.BindVisible()).To(vm => vm.IsBusy);

            bindingSet.Apply();
        }

        private void InitializeScrollView()
        {
            ScrollView.ContentInset = new UIEdgeInsets(0f, 0f, 82f, 0f);
        }

        private void InitializeOrderNumberTextField()
        {
            OrderNumberTextField.ReturnKeyType = UIReturnKeyType.Next;
            OrderNumberTextField.ShouldReturn = OnOrderNumberTextFieldShouldReturn;
        }

        private void InitializeCollectionView()
        {
            _source = new CollectionViewSource(PhotosCollectionView)
                .Register<FeedbackPhotoItemViewModel>(FeedbackPhotoItemViewCell.Nib, FeedbackPhotoItemViewCell.Key);

            PhotosCollectionView.Source = _source;
            PhotosCollectionView.Delegate = new FeedbackPhotosCollectionViewDelegateFlowLayout();
        }

        private void OnViewTap()
        {
            View.EndEditing(true);
        }

        private bool OnOrderNumberTextFieldShouldReturn(UITextField _)
        {
            QuestionTextView.BecomeFirstResponder();
            return true;
        }

        private void AddKeyboardObservers()
        {
            keyboardWillShowObject = UIKeyboard.Notifications.ObserveWillShow(KeyboardWillShow);
            keyboardWillHideObject = UIKeyboard.Notifications.ObserveWillHide(KeyboardWillHide);
        }

        private void RemoveKeyboardObservers()
        {
            keyboardWillShowObject.Dispose();
            keyboardWillHideObject.Dispose();
        }

        private void KeyboardWillShow(object _, UIKeyboardEventArgs args)
        {
            var height = args.FrameEnd.Height;
            var offset = height - UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom - MainViewController.TabHeight;
            SetScrollViewBottomConstraint(offset, args.AnimationDuration);
        }

        private void KeyboardWillHide(object _, UIKeyboardEventArgs args)
        {
            SetScrollViewBottomConstraint(0f, args.AnimationDuration);
        }

        private void SetScrollViewBottomConstraint(nfloat offset, double animationDuration)
        {
            UIView.Animate(animationDuration, () =>
            {
                ScrollViewBottomConstraint.Constant = offset;
                View.LayoutIfNeeded();
            });
        }
    }
}
