using System;
using System.Linq;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using SushiShop.Core.Extensions;
using SushiShop.Core.ViewModels.Menu.Items;
using SushiShop.Ios.Common;
using SushiShop.Ios.Delegates;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Sources;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Menu
{
    public partial class MenuPromotionListItemViewCell : BaseCollectionViewCell
    {
        public static readonly NSString Key = new NSString(nameof(MenuPromotionListItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private CollectionViewSource viewSource;
        private IndicatorView[] indicators;

        protected MenuPromotionListItemViewCell(IntPtr handle)
            : base(handle)
        {
        }

        private int itemsCount;
        public int ItemsCount
        {
            get => itemsCount;
            set
            {
                itemsCount = value;
                InitializeStackViewItems(itemsCount);
            }
        }

        private IndicatorView selectedIndicator;
        private IndicatorView SelectedIndicator
        {
            get => selectedIndicator;
            set
            {
                if (selectedIndicator != null)
                {
                    selectedIndicator.IsSelected = false;
                }

                selectedIndicator = value;
                if (selectedIndicator != null)
                {
                    selectedIndicator.IsSelected = true;
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            viewSource = new CollectionViewSource(CollectionView)
                .Register<MenuPromotionItemViewModel>(MenuPromotionItemViewCell.Nib, MenuPromotionItemViewCell.Key);

            CollectionView.Source = viewSource;
            CollectionView.Delegate = new GroupsMenuItemCollectionViewDelegateFlowLayout(OnScrolled);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<MenuPromotionListItemViewCell, MenuPromotionListItemViewModel>();
            bindingSet.Bind(viewSource).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(this).For(v => v.ItemsCount).To(vm => vm.ItemsCount);
            bindingSet.Apply();
        }

        private IndicatorView CreateIndicatorView()
        {
            var view = new IndicatorView();
            view.TranslatesAutoresizingMaskIntoConstraints = false;

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                view.WidthAnchor.ConstraintEqualTo(IndicatorView.Width),
                view.HeightAnchor.ConstraintEqualTo(IndicatorView.Height)
            });

            return view;
        }

        private void InitializeStackViewItems(int itemsCount)
        {
            StackView.ArrangedSubviews.ForEach(view => view.RemoveFromSuperview());
            indicators = null;
            SelectedIndicator = null;

            if (itemsCount == 0)
            {
                return;
            }

            indicators = Enumerable
                .Range(0, itemsCount)
                .Select(_ => CreateIndicatorView())
                .Pipe(StackView.AddArrangedSubview)
                .ToArray();

            SelectedIndicator = indicators[0];
        }

        private void OnScrolled()
        {
            var indexPath = CollectionView.GetCenterIndexPathOrDefault();
            if (indexPath != null)
            {
                SelectedIndicator = indicators[indexPath.Row];
            }
        }

        private class IndicatorView : UIView
        {
            public const float Height = 10f;
            public const float Width = 10f;

            private const float InnerViewHeight = 4f;
            private const float InnerViewWidth = 4f;

            private readonly UIView innerView;

            public IndicatorView()
            {
                BackgroundColor = Colors.White;
                Layer.CornerRadius = Height / 2f;

                innerView = CreateInnerView();
            }

            private bool isSelected;
            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    isSelected = value;
                    innerView.BackgroundColor = isSelected ? Colors.FigmaBlack : Colors.Gray4;
                }
            }

            private UIView CreateInnerView()
            {
                var innerView = new UIView();
                innerView.TranslatesAutoresizingMaskIntoConstraints = false;
                innerView.BackgroundColor = Colors.Gray4;
                innerView.Layer.CornerRadius = InnerViewHeight / 2f;

                AddSubview(innerView);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    innerView.CenterXAnchor.ConstraintEqualTo(CenterXAnchor),
                    innerView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor),
                    innerView.WidthAnchor.ConstraintEqualTo(InnerViewWidth),
                    innerView.HeightAnchor.ConstraintEqualTo(InnerViewHeight)
                });

                return innerView;
            }
        }
    }
}
