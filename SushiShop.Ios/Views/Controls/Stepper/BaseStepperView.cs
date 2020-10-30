using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.ViewModels.Common;
using SushiShop.Ios.Common;
using SushiShop.Ios.Extensions;
using SushiShop.Ios.Views.Controls.Abstract;
using UIKit;

namespace SushiShop.Ios.Views.Controls.Stepper
{
    public abstract class BaseStepperView : BaseView<StepperViewModel>
    {
        private UIStackView stackView;
        private UIButton removeButton;
        private UILabel titleLabel;
        private UIButton addButton;

        private CAGradientLayer gradientLayer;
        private UITapGestureRecognizer tapGestureRecognizer;
        private NSLayoutConstraint _widthConstraint;

        protected BaseStepperView()
        {
        }

        protected BaseStepperView(IntPtr handle)
            : base(handle)
        {
        }

        public string Title { get; set; }

        private int count;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                if (count == 0)
                {
                    if (Title is null)
                    {
                        removeButton.Hidden = true;
                        addButton.Hidden = false;
                        titleLabel.Hidden = true;
                        RemoveTapGestureRecognizer();
                    }
                    else
                    {
                        removeButton.Hidden = true;
                        addButton.Hidden = true;
                        SetTitle(Title);
                        AddTapGestureRecognizer();
                    }
                }
                else
                {
                    removeButton.Hidden = false;
                    addButton.Hidden = false;
                    SetTitle(count.ToString());
                    RemoveTapGestureRecognizer();
                }

                UpdateWidthIfNeeded();
            }
        }

        protected abstract nfloat ButtonWidth { get; }

        protected abstract nfloat TextSize { get; }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            gradientLayer.Frame = Bounds;
        }

        protected override void Initialize()
        {
            base.Initialize();

            ClipsToBounds = true;
            Layer.CornerRadius = Bounds.Height / 2f;

            _widthConstraint = Constraints.FirstOrDefault(x => x.FirstAttribute == NSLayoutAttribute.Width);

            InitializeGradientLayer();
            InitializeStackView();
            InitializeRemoveButton();
            InitializeTitleLabel();
            InitializeAddButton();
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<BaseStepperView, StepperViewModel>();

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
            bindingSet.Bind(this).For(v => v.Count).To(vm => vm.Count);
            bindingSet.Bind(removeButton).For(v => v.BindTouchUpInside()).To(vm => vm.RemoveCommand);
            bindingSet.Bind(addButton).For(v => v.BindTouchUpInside()).To(vm => vm.AddCommand);

            bindingSet.Apply();
        }

        private void InitializeGradientLayer()
        {
            gradientLayer = new CAGradientLayer
            {
                Colors = new[]
                {
                    Colors.OrangeGradientStart.CGColor,
                    Colors.OrangeGradientEnd.CGColor,
                },
                StartPoint = new CGPoint(0f, 1f),
                EndPoint = new CGPoint(1f, 1f)
            };

            Layer.AddSublayer(gradientLayer);
        }

        private void InitializeStackView()
        {
            stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill
            };

            AddSubview(stackView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                stackView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                stackView.TopAnchor.ConstraintEqualTo(TopAnchor),
                stackView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
                stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor)
            });
        }

        private void InitializeRemoveButton()
        {
            removeButton = CreateButton(ImageNames.Minus);
            stackView.AddArrangedSubview(removeButton);
        }

        private void InitializeTitleLabel()
        {
            titleLabel = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                TextAlignment = UITextAlignment.Center,
                Lines = 1,
                TextColor = Colors.White,
                Font = Font.Create(FontStyle.Medium, TextSize)
            };

            stackView.AddArrangedSubview(titleLabel);
        }

        private void InitializeAddButton()
        {
            addButton = CreateButton(ImageNames.Plus);
            stackView.AddArrangedSubview(addButton);
        }

        private UIButton CreateButton(string imageName)
        {
            var button = new UIButton();
            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.WidthAnchor.ConstraintEqualTo(ButtonWidth).Active = true;

            var image = UIImage.FromBundle(imageName);
            button.SetImage(image, UIControlState.Normal);
            button.SetImage(image.WithAlpha(0.5f), UIControlState.Highlighted);

            return button;
        }

        private void SetTitle(string title)
        {
            titleLabel.Text = title;
            titleLabel.Hidden = false;
        }

        private void AddTapGestureRecognizer()
        {
            tapGestureRecognizer = new UITapGestureRecognizer(() => ViewModel.AddCommand.Execute());
            AddGestureRecognizer(tapGestureRecognizer);
        }

        private void RemoveTapGestureRecognizer()
        {
            if (tapGestureRecognizer != null)
            {
                RemoveGestureRecognizer(tapGestureRecognizer);
                tapGestureRecognizer = null;
            }
        }

        private void UpdateWidthIfNeeded()
        {
            if (_widthConstraint is null)
            {
                return;
            }

            var titleLabelWidth = titleLabel.Hidden ? 0f : titleLabel.SizeThatFits(CGSize.Empty).Width;
            var removeButtonWidth = removeButton.Hidden ? 0f : ButtonWidth;
            var addButtonWidth = addButton.Hidden ? 0f : ButtonWidth;
            _widthConstraint.Constant = titleLabelWidth + removeButtonWidth + addButtonWidth;

            CATransaction.DisableActions = true;
            LayoutIfNeeded();
            CATransaction.DisableActions = false;
        }
    }
}
