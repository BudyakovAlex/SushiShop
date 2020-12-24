using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Extensions;
using UIKit;

namespace SushiShop.Ios.Sources
{
    public class GenderPickerViewModel : MvxPickerViewModel
    {
        public GenderPickerViewModel(UIPickerView pickerView) : base(pickerView)
        {
        }

        protected override string RowTitle(nint row, object item)
        {
            if (item is GenderType genderType)
            {
                return genderType.ToLocalizedString();
            }
            else
            {
                return base.RowTitle(row, item);
            }
        }
    }
}
