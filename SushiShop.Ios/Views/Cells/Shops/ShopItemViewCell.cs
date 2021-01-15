using System;
using System.Collections.Generic;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.UIKit.Views.Cells;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using SushiShop.Core.ViewModels.Shops.Items;
using SushiShop.Ios.Extensions;
using UIKit;

namespace SushiShop.Ios.Views.Cells.Shops
{
    public partial class ShopItemViewCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(ShopItemViewCell));
        public static readonly UINib Nib = UINib.FromName(Key, NSBundle.MainBundle);

        private readonly Dictionary<NSRange, string> _phonesRangesMappings = new Dictionary<NSRange, string>();

        protected ShopItemViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected ShopItemViewModel ViewModel => DataContext as ShopItemViewModel;

        public string[] Phones
        {
            set => SetPhonesAttributedString(value);
        }

        protected override void Bind()
        {
            base.Bind();

            var bindingSet = this.CreateBindingSet<ShopItemViewCell, ShopItemViewModel>();

            bindingSet.Bind(AddressLabel).For(v => v.Text).To(vm => vm.LongTitle);
            bindingSet.Bind(this).For(nameof(Phones)).To(vm => vm.Phones);
            bindingSet.Bind(WorkingTimeLabel).For(v => v.Text).To(vm => vm.WorkingTime);
            bindingSet.Bind(MetroButton).For(v => v.BindTouchUpInside()).To(vm => vm.ShowNearestMetroCommand);
            bindingSet.Bind(MetroButton).For(v => v.BindVisible()).To(vm => vm.HasNearestMetro);
            bindingSet.Bind(MapButton).For(v => v.BindTouchUpInside()).To(vm => vm.GoToMapCommand);

            bindingSet.Apply();
        }

        private void SetPhonesAttributedString(string[] phones)
        {
            var joinedText = string.Join(", ", phones);
            var attributedText = new NSMutableAttributedString(joinedText);
            foreach (var item in phones)
            {
                var startPosition = joinedText.IndexOf(item);
                var endPosition = startPosition + item.Length;
                _phonesRangesMappings[new NSRange(startPosition, endPosition)] = item;
            }

            PhoneLabel.AttributedText = attributedText;
            PhoneLabel.UserInteractionEnabled = true;
            PhoneLabel.AddGestureRecognizer(new UITapGestureRecognizer(OnPhoneLabelTapped));
        }

        private void OnPhoneLabelTapped(UITapGestureRecognizer gesture)
        {
            foreach (var item in _phonesRangesMappings)
            {
                if (gesture.DidTapAttributedTextInLabel(PhoneLabel, item.Key))
                {
                    ViewModel?.CallCommand?.Execute(item.Value);
                    break;
                }
            }
        }
    }
}
