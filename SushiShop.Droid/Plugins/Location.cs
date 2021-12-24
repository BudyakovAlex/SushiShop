using System.Threading.Tasks;
using Android.Content;
using SushiShop.Core.Plugins;

namespace SushiShop.Droid.Plugins
{
    public class Location : ILocation
    {
        public const int LocationActivityResult = 2543;

        public static TaskCompletionSource<bool> CompletionSource { get; private set; }

        public Task RequestEnableLocationServiceAsync()
        {
            CompletionSource?.TrySetResult(false);
            CompletionSource = new TaskCompletionSource<bool>();
            var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            if (Xamarin.Essentials.Platform.CurrentActivity != null)
            {
                Xamarin.Essentials.Platform.CurrentActivity.StartActivityForResult(intent, LocationActivityResult);
            }

            return CompletionSource.Task;
        }
    }
}
