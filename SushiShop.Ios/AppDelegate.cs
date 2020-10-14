using Foundation;
using MvvmCross.Platforms.Ios.Core;
using SushiShop.Core;

namespace SushiShop.Ios
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}

