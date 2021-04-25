using Android.Text;
using Android.Widget;
using Java.Lang;
using SushiShop.Core.Common;
using SushiShop.Core.Extensions;
using System.Linq;

namespace SushiShop.Droid.Platform.Watchers
{
    public class PhoneTextWatcher : Object, ITextWatcher
    {
        private readonly EditText editText;

        public PhoneTextWatcher(EditText editText)
        {
            this.editText = editText;
        }

        public void AfterTextChanged(IEditable s)
        {
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            var changedString = (start > 0 ? editText.Text.Substring(0, start) : "")
                + s.ToString()
                + editText.Text.Substring(
                    start + count,
                    editText.Text.Length - start - count);

            if (count == 1 && s.Length() == 0 &&
                !char.IsDigit(editText.Text.ElementAtOrDefault(start)))
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

            editText.Text = changedString.FilterByMask(Constants.Format.Phone.MaskFormat);
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }
    }
}
