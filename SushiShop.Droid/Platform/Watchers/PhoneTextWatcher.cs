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
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            editText.RemoveTextChangedListener(this);

            var selectionPosition = before < count
                ? editText.Text.Length
                : editText.SelectionStart;

            editText.Text = editText.Text.FilterByMask(Constants.Format.Phone.MaskFormat);
            if (s.Length() < 4)
            {
                editText.SetSelection(editText.Text.Length);
            }
            else
            {
                editText.SetSelection(Math.Min(selectionPosition, editText.Text.Length));
            }

            editText.AddTextChangedListener(this);
        }
    }
}