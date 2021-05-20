using System;
using Android.Runtime;
using Android.Views;
using Java.Interop;

namespace SushiShop.Droid.Views.Listeners
{
    public class ViewOnKeyListener : Java.Lang.Object, View.IOnKeyListener, IJavaObject, IDisposable, IJavaPeerable
    {
        private readonly Func<View, Keycode, KeyEvent, bool> func;

        public ViewOnKeyListener(Func<View, Keycode, KeyEvent, bool> func)
        {
            this.func = func;
        }

        public bool OnKey(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (func != null)
            {
                return func.Invoke(v, keyCode, e);
            }

            return false;
        }
    }
}
