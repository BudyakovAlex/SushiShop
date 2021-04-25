using Foundation;
using SushiShop.Core.Common;
using SushiShop.Core.Extensions;
using UIKit;

namespace SushiShop.Ios.Formatters
{
    public class PhoneNumberFormatter
    {
        public PhoneNumberFormatter(UITextField textField)
        {
            textField.ShouldChangeCharacters = ShouldChangePhoneNumber;
        }

        private bool ShouldChangePhoneNumber(UITextField textField, NSRange range, string replacementString)
        {
            var changedString = (range.Location > 0 ? textField.Text.Substring(0, (int)range.Location) : "")
                + replacementString
                + textField.Text.Substring(
                    (int)(range.Location + range.Length),
                    (int)(textField.Text.Length - range.Location - range.Length));

            if (range.Length == 1 && replacementString.Length == 0 &&
                !char.IsDigit(textField.Text[(int)range.Location]))
            {
                // Something was deleted.  Delete past the previous number
                int location = changedString.Length - 1;
                if (location > 0)
                {
                    for (; location > 0; location--)
                    {
                        if (char.IsDigit(changedString[location]))
                        {
                            break;
                        }
                    }

                    changedString = changedString.Substring(0, location);
                }
            }

            textField.Text = changedString.FilterByMask(Constants.Format.Phone.MaskFormat);
            return false;
        }
    }
}