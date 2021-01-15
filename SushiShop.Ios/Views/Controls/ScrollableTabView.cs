using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Extensions;
using SushiShop.Ios.Common;
using SushiShop.Ios.Views.Controls.Abstract;
using UIKit;

namespace SushiShop.Ios.Views.Controls
{
    [Register(nameof(ScrollableTabView))]
    public class ScrollableTabView : BaseView
    {
        private const float MinScrollOffset = 7f;

        private UIStackView stackView;
        private TabItemView[] tabs;
        private NSLayoutConstraint trailingConstraint;

        public ScrollableTabView()
        {
        }

        public ScrollableTabView(CGRect frame)
            : base(frame)
        {
        }

        protected ScrollableTabView(IntPtr handle)
            : base(handle)
        {
        }

        public Action OnTabChangedAfterTapAction { get; set; }

        private bool isFixedTabs;
        public bool IsFixedTabs
        {
            get => isFixedTabs;
            set
            {
                if (value == isFixedTabs)
                {
                    return;
                }

                isFixedTabs = value;
                trailingConstraint.Active = value;
                SetFixedTabs();
            }
        }

        private List<string> items;
        public List<string> Items
        {
            get => items;
            set
            {
                items = value;
                stackView.ArrangedSubviews.ForEach(view => view.RemoveFromSuperview());

                if (items is null || items.Count == 0)
                {
                    tabs = null;
                }
                else
                {
                    tabs = items
                        .Select(CreateTabItem)
                        .Pipe(stackView.AddArrangedSubview)
                        .ToArray();

                    SetFixedTabs();
                }

                SelectedIndex = 0;
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                var oldTab = tabs?.ElementAtOrDefault(selectedIndex);
                if (oldTab != null)
                {
                    oldTab.IsSelected = false;
                }

                selectedIndex = value;
                RaisePropertyChanged();

                var newTab = tabs?.ElementAtOrDefault(selectedIndex);
                if (newTab != null)
                {
                    newTab.IsSelected = true;
                    ScrollTo(newTab);
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeStackView();
        }

        private void InitializeStackView()
        {
            stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill,
                DirectionalLayoutMargins = new NSDirectionalEdgeInsets(0f, MinScrollOffset, 0f, 0f),
                LayoutMarginsRelativeArrangement = true
            };

            AddSubview(stackView);

            trailingConstraint = stackView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                stackView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                stackView.TopAnchor.ConstraintEqualTo(TopAnchor),
                stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
            });
        }

        private TabItemView CreateTabItem(string text, int index) =>
            new TabItemView(text, index, OnTabTapped);

        private void OnTabTapped(int index)
        {
            SelectedIndex = index;
            OnTabChangedAfterTapAction?.Invoke();
        }

        private void ScrollTo(TabItemView tab)
        {
            if (IsFixedTabs)
            {
                return;
            }

            if (tab.Index == 0)
            {
                SetScrollOffset(MinScrollOffset);
            }
            else
            {
                var tabX = stackView.ArrangedSubviews
                    .Take(tab.Index)
                    .Select(view => (float) view.Bounds.Width)
                    .Sum();

                var expectedOffset = tabX - ((Bounds.Width - tab.Bounds.Width) / 2f);
                var offset = expectedOffset < MinScrollOffset ? MinScrollOffset : -expectedOffset;

                SetScrollOffset(offset);
            }
        }

        private void SetScrollOffset(nfloat left)
        {
            stackView.LayoutMargins = new UIEdgeInsets(0f, left, 0f, 0f);
            Animate(0.5d, 0f, UIViewAnimationOptions.AllowUserInteraction, stackView.LayoutIfNeeded, null);
        }

        private void SetFixedTabs()
        {
            if (!IsFixedTabs || tabs == null)
            {
                stackView.DirectionalLayoutMargins = new NSDirectionalEdgeInsets(0f, MinScrollOffset, 0f, 0f);
                return;
            }

            stackView.DirectionalLayoutMargins = new NSDirectionalEdgeInsets(0f, 0f, 0f, 0f);

            tabs
                .Pipe(tab =>
                {
                    tab.WidthAnchor.ConstraintEqualTo(stackView.WidthAnchor, 1.0f / Items.Count).Active = true;
                    tab.SetFixedFormat();
                })
                .ToArray();
        }

        private class TabItemView : UIView
        {
            private const float IndicatorHeight = 4f;

            private UIView indicator;
            private UIButton button;

            public TabItemView(string title, int index, Action<int> onTap)
            {
                Index = index;

                InitializeButton(title, onTap);
                InitializeIndicator();
            }

            public int Index { get; }

            private bool isSelected;
            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    isSelected = value;
                    indicator.Hidden = !isSelected;
                }
            }

            public void SetFixedFormat()
            {
                button.TitleLabel.Lines = 0;
                button.TitleLabel.TextAlignment = UITextAlignment.Center;
            }

            private void InitializeButton(string title, Action<int> onTap)
            {
                button = new UIButton();
                button.TranslatesAutoresizingMaskIntoConstraints = false;
                button.Font = Font.Create(FontStyle.Medium, 16f);
                button.ContentEdgeInsets = new UIEdgeInsets(0f, 12f, 0f, 12f);
                button.SetTitle(title, UIControlState.Normal);
                button.AddGestureRecognizer(new UITapGestureRecognizer(() => onTap(Index)));
                button.SetTitleColor(Colors.FigmaBlack, UIControlState.Normal);
                button.SetTitleColor(Colors.FigmaBlack.ColorWithAlpha(0.5f), UIControlState.Highlighted);

                AddSubview(button);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    button.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                    button.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                    button.CenterYAnchor.ConstraintEqualTo(CenterYAnchor)
                });
            }

            private void InitializeIndicator()
            {
                indicator = new UIView();
                indicator.TranslatesAutoresizingMaskIntoConstraints = false;
                indicator.Hidden = true;
                indicator.BackgroundColor = Colors.Orange2;
                indicator.Layer.CornerRadius = IndicatorHeight / 2f;

                AddSubview(indicator);

                NSLayoutConstraint.ActivateConstraints(new[]
                {
                    indicator.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                    indicator.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                    indicator.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                    indicator.HeightAnchor.ConstraintEqualTo(IndicatorHeight)
                });
            }
        }
    }
}
